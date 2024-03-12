using WTS.Enums;
using WTS.Services.Interfaces;

namespace WTS.Services
{
    public class FoodRandomizerService : IFoodRandomizerService
    {
        private readonly Random _random;

        private readonly IEnumerable<string> _carnivoreFoods =
        [
            "Raw meats", "Fish", "Small rodents", "Insects", "Bird eggs",
            "Poultry", "Organ meats", "Bones with marrow", "Live prey", "Shellfish"
        ];

        private readonly IEnumerable<string> _herbivoreFoods =
        [
            "Fresh leaves", "Root vegetables", "Tree bark", "Fruits", "Flowers and buds",
            "Grasses", "Legumes", "Hay", "Herbs", "Nuts and seeds"
        ];

        private readonly IEnumerable<string> _omnivoreFoods =
        [
            "Beans", "Eggs", "Fresh fruits", "Vegetables", "Grains",
            "Nuts and seeds", "Insects", "Dairy products", "Fish", "Root vegetables"
        ];

        public FoodRandomizerService()
        {
            _random = new Random();
        }

        /// <summary>
        /// Retrieves a randomized collection of food items based on the specified eater type.
        /// </summary>
        /// <param name="eaterType">The eater type of the animal.</param>
        /// <param name="itemsPerMeal">The number of food items to include per meal.</param>
        /// <returns>An enumerable collection of food items.</returns>
        public IEnumerable<string> GetRandomFoodItems(EaterType eaterType, int itemsPerMeal)
        {
            IEnumerable<string> foodChoices = eaterType switch
            {
                EaterType.Carnivore => _carnivoreFoods,
                EaterType.Herbivore => _herbivoreFoods,
                EaterType.Omnivore => _omnivoreFoods,
            };

            return foodChoices.OrderBy(_ => _random.Next()).Take(itemsPerMeal);
        }
    }
}