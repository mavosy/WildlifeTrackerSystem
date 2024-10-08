using System.IO;
using System.Text;
using System.Xml.Serialization;
using WTS.Models;
using WTS.Services.Interfaces;

namespace WTS.Services
{
    /// <summary>
    /// Manages the relationships between food items and animals, allows associating animals with specific food items.
    /// </summary>
    public class FoodManager : IFoodManager
    {
        private readonly Dictionary<string, List<string>> _foodToAnimalsMap;
        private readonly IFileService _fileService;

        public FoodManager(IFileService fileService)
        {
            _foodToAnimalsMap = new Dictionary<string, List<string>>();
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        /// <summary>
        /// Adds a new food item to the manager. If the food item already exists, nothing happens.
        /// </summary>
        /// <param name="foodItem">The food item to add.</param>
        public void AddFoodItem(FoodItem foodItem)
        {
            if (!_foodToAnimalsMap.ContainsKey(foodItem.Name))
            {
                _foodToAnimalsMap[foodItem.Name] = new List<string>();
            }
        }

        /// <summary>
        /// Associates an animal with a specified food item.
        /// </summary>
        /// <param name="animal">The animal to associate. If the animal has no name, uses Id instead</param>
        /// <param name="foodItemName">The name of the food item to associate the animal with.</param>
        /// <returns>true if the association is successfully made. Otherwise, false.</returns>
        public void PairAnimalWithFoodItem(string animalName, string foodItemName)
        {
            if (!_foodToAnimalsMap.ContainsKey(foodItemName))
            {
                _foodToAnimalsMap[foodItemName] = [animalName];
            }

            _foodToAnimalsMap[foodItemName].Add(animalName);
        }

        /// <summary>
        /// Retrieves a list of animal names associated with a specified food item.
        /// </summary>
        /// <param name="foodItemName">The name of the food item for which to retrieve associated animals.</param>
        /// <returns>An enumerable collection of animal names associated with the specified food item. If no associations exist, an empty list is returned.</returns>
        public IEnumerable<string> GetAnimalsForFoodItem(string foodItemName)
        {
            if (_foodToAnimalsMap.ContainsKey(foodItemName))
            {
                return _foodToAnimalsMap[foodItemName];
            }
            return new List<string>();
        }

        /// <summary>
        /// Retrieves a list of food items associated with a specified animal.
        /// </summary>
        /// <param name="animalName">The name or ID (DisplayName property) of the animal for which to retrieve associated food items.</param>
        /// <returns>An enumerable collection of food items associated with the specified animal. If no associations exist, an empty list is returned.</returns>
        public IEnumerable<string> GetFoodItemsForAnimal(string animalName)
        {
            return _foodToAnimalsMap
                .Where(pair => pair.Value.Contains(animalName))
                .Select(pair => pair.Key);
        }

        public IEnumerable<KeyValuePair<string, List<string>>> GetFoodToAnimalsMap()
        {
            return _foodToAnimalsMap.Select(entry => new KeyValuePair<string, List<string>>(entry.Key, new List<string>(entry.Value)));
        }

        public void SaveAsXml(string fileName, List<string> animalNames)
        {
            try
            {
                var animalFoodData = new AnimalFoodData();
                var foodToAnimalsMap = GetFoodToAnimalsMap();

                foreach (var animalName in animalNames)
                {
                    var foodItems = foodToAnimalsMap
                        .Where(entry => entry.Value.Contains(animalName))
                        .Select(entry => entry.Key)
                        .ToList();

                    if (foodItems.Count != 0)
                    {
                        animalFoodData.Entries.Add(new AnimalFoodEntry
                        {
                            AnimalName = animalName,
                            FoodItems = foodItems
                        });
                    }
                }

                if (animalFoodData.Entries.Count != 0)
                {
                    _fileService.SaveDataToXmlFile(fileName, animalFoodData);
                }
                else
                {
                    throw new Exception("No animals with registered food.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be saved", ex.InnerException);
            }
        }

        public string LoadFromXml(string fileName)
        {
            try
            {
                var animalFoodData = _fileService.LoadDataFromXmlFile<AnimalFoodData>(fileName);
                if (animalFoodData.Entries.Count != 0)
                {
                    return FormatAnimalFoodItems(animalFoodData);
                }
                else
                {
                    throw new Exception("No data found in the file.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be loaded", ex);
            }
        }

        public static string FormatAnimalFoodItems(AnimalFoodData animalFoodData)
        {
            var builder = new StringBuilder();
            foreach (var entry in animalFoodData.Entries)
            {
                builder.AppendLine($"Animal: {entry.AnimalName}");
                foreach (var foodItem in entry.FoodItems)
                {
                    builder.AppendLine($"  FoodItem: {foodItem}");
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}