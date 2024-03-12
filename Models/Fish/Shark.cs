using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Fish
{
    public class Shark : Fish
    {
        private FoodSchedule _foodSchedule;

        public Shark(string id, string? name, int? age, GenderType gender, WaterHabitatType habitat, int? numberOfGills)
            : base(id, name, age, gender, habitat)
        {
            NumberOfGills = numberOfGills;
            SetFoodSchedule();
        }

        public int? NumberOfGills { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("NumberOfGills", ValueWrapper.Create(NumberOfGills));
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