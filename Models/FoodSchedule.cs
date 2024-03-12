using WTS.Enums;
using WTS.Services;
using WTS.Services.Interfaces;

namespace WTS.Models
{
    /// <summary>
    /// Represents a food schedule for different types of food consumers, and holding a list of food items.
    /// </summary>
    public class FoodSchedule
    {
        private readonly List<string> _foodList;
        private readonly IFoodRandomizerService _foodService;

        public EaterType EaterType { get; set; }
        public int Count => _foodList.Count;

        public FoodSchedule()
        {
            _foodList = new List<string>();
            _foodService = new FoodRandomizerService();
        }

        /// <summary>
        /// Initializes the food schedule with randomized meal plans for morning, lunch, and evening.
        /// </summary>
        public void InitializeSchedule()
        {
            _foodList.Add("Morning: " + String.Join(" and ", _foodService.GetRandomFoodItems(EaterType, 2)));
            _foodList.Add("Lunch: " + String.Join(" and ", _foodService.GetRandomFoodItems(EaterType, 2)));
            _foodList.Add("Evening: " + String.Join(" and ", _foodService.GetRandomFoodItems(EaterType, 2)));
        }

        /// <summary>
        /// Adds a new food foodItem to the schedule.
        /// </summary>
        /// <param name="foodItem">The food foodItem to add.</param>
        public void Add(string foodItem) => _foodList.Add(foodItem);

        /// <summary>
        /// Changes a food item at a specified index.
        /// </summary>
        /// <param name="index">The index to change the food item at.</param>
        /// <param name="item">The new food item.</param>
        /// <returns>True if the change is successful, false if the index is out of range.</returns>
        public bool ChangeAt(int index, string item)
        {
            if (!IsIndexInRange(index))
            {
                return false;
            }

            _foodList[index] = item;
            return true;
        }

        /// <summary>
        /// Deletes a food item at a specified index.
        /// </summary>
        /// <param name="index">The index of the food item to delete.</param>
        /// <returns>True if deletion is successful, false if the index is out of range.</returns>
        public bool DeleteAt(int index)
        {
            if (!IsIndexInRange(index)) { return false; }

            _foodList.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Retrieves the food schedule as an array of strings.
        /// </summary>
        /// <returns>An array of food items in the schedule.</returns>
        public string[] GetFoodListInfoStrings() => _foodList.ToArray();

        /// <summary>
        /// Generates a title string for the food schedule.
        /// </summary>
        /// <returns>A string representing the title of the food schedule.</returns>
        public string Title() => $"Food Schedule for {EaterType}";

        /// <summary>
        /// Provides a string representation of the food schedule.
        /// </summary>
        /// <returns>A string with all food items, separated by new lines.</returns>
        public override string ToString() => string.Join("\n", _foodList);

        /// <summary>
        /// Checks if a given index is within the range of the food list.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if the index is within range, false otherwise.</returns>
        public bool IsIndexInRange(int index) => index >= 0 && index < _foodList.Count;
    }
}