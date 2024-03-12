using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Mammals
{
    public class Elephant : Mammal
    {
        private FoodSchedule _foodSchedule;
        public Elephant(string id, string? name, int? age, GenderType gender, int? numberOfLegs, int? trunkLength)
            : base(id, name, age, gender, numberOfLegs)
        {
            TrunkLength = trunkLength;
            SetFoodSchedule();
        }

        /// <summary>
        /// Trunk length in centimeters
        /// </summary>
        public int? TrunkLength { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("TrunkLength", ValueWrapper.Create(TrunkLength));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Trumpet";
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