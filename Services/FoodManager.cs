using WTS.Models;
using WTS.Services.Interfaces;

namespace WTS.Services
{
    /// <summary>
    /// Manages the relationships between food items and animals, allows associating animals with specific food items.
    /// </summary>
    public class FoodManager : IFoodManager
    {
        private Dictionary<string, List<string>> _foodToAnimalsMap;

        public FoodManager()
        {
            _foodToAnimalsMap = new Dictionary<string, List<string>>();
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
    }
}