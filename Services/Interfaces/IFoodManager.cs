using WTS.Models;

namespace WTS.Services.Interfaces
{
    /// <summary>
    /// Manages the relationships between food items and animals, allows associating animals with specific food items.
    /// </summary>
    public interface IFoodManager
    {
        /// <summary>
        /// Adds a new food item to the manager. If the food item already exists, nothing happens.
        /// </summary>
        /// <param name="foodItem">The food item to add.</param>
        void AddFoodItem(FoodItem foodItem);

        /// <summary>
        /// Associates an animal with a specified food item.
        /// </summary>
        /// <param name="animal">The animal to associate. If the animal has no name, uses Id instead</param>
        /// <param name="foodItemName">The name of the food item to associate the animal with.</param>
        /// <returns>true if the association is successfully made. Otherwise, false.</returns>
        void PairAnimalWithFoodItem(string animalName, string foodItemName);

        /// <summary>
        /// Retrieves a list of animal names associated with a specified food item.
        /// </summary>
        /// <param name="foodItemName">The name of the food item for which to retrieve associated animals.</param>
        /// <returns>An enumerable collection of animal names associated with the specified food item. If no associations exist, an empty list is returned.</returns>
        IEnumerable<string> GetAnimalsForFoodItem(string foodItemName);

        /// <summary>
        /// Retrieves a list of food items associated with a specified animal.
        /// </summary>
        /// <param name="animalName">The name or ID (DisplayName property) of the animal for which to retrieve associated food items.</param>
        /// <returns>An enumerable collection of food items associated with the specified animal. If no associations exist, an empty list is returned.</returns>
        IEnumerable<string> GetFoodItemsForAnimal(string animalName);

        IEnumerable<KeyValuePair<string, List<string>>> GetFoodToAnimalsMap();
    }
}