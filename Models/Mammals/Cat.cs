using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Mammals
{
    public class Cat : Mammal
    {
        private FoodSchedule _foodSchedule;

        public Cat(string id, string? name, int? age, GenderType gender, int? numberOfLegs, string? breed)
            : base(id, name, age, gender, numberOfLegs)
        {
            Breed = breed;
            SetFoodSchedule();
        }

        public string? Breed { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Breed", ValueWrapper.Create(Breed));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Meow";
        }

        /// <summary>
        /// Sets the food schedule and food consumption category for the animal.
        /// </summary>
        private void SetFoodSchedule()
        {
            _foodSchedule = new FoodSchedule();
            _foodSchedule.EaterType = EaterType.Carnivore;
            _foodSchedule.InitializeSchedule();
        }

        public override FoodSchedule GetFoodSchedule()
        {
            return _foodSchedule;
        }
    }
}