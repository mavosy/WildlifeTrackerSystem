using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Insects
{
    public class Ladybug : Insect
    {
        private FoodSchedule _foodSchedule;

        public Ladybug(string id, string? name, int? age, GenderType gender, bool canFly, int? numberOfSpots)
            : base(id, name, age, gender, canFly)
        {
            NumberOfSpots = numberOfSpots;
            SetFoodSchedule();
        }

        public int? NumberOfSpots { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("NumberOfSpots", ValueWrapper.Create(NumberOfSpots));
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
            _foodSchedule.EaterType = EaterType.Carnivore;
            _foodSchedule.InitializeSchedule();
        }

        public override FoodSchedule GetFoodSchedule()
        {
            return _foodSchedule;
        }
    }
}