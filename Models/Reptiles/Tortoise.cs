using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Reptiles
{
    public class Tortoise : Reptile
    {
        private FoodSchedule _foodSchedule;

        public Tortoise(string id, string? name, int? age, GenderType gender, bool hasScales, int? maxAgeInYears)
            : base(id, name, age, gender, hasScales)
        {
            MaxAgeInYears = maxAgeInYears;
            SetFoodSchedule();
        }

        public int? MaxAgeInYears { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("MaxAgeInYears", ValueWrapper.Create(MaxAgeInYears));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Silent";
        }

        /// <summary>
        /// Sets the food schedule and food consumption category for the animal.
        /// </summary>
        private void SetFoodSchedule()
        {
            _foodSchedule = new FoodSchedule();
            _foodSchedule.EaterType = EaterType.Herbivore;
            _foodSchedule.InitializeSchedule();
        }

        public override FoodSchedule GetFoodSchedule()
        {
            return _foodSchedule;
        }
    }
}