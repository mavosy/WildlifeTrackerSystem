using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Amphibians
{
    public class Frog : Amphibian
    {
        private FoodSchedule _foodSchedule;

        public Frog(string id, string? name, int? age, GenderType gender, bool landliving, string? color)
            : base(id, name, age, gender, landliving)
        {
            Color = color;
            SetFoodSchedule();
        }

        public string? Color { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Color", ValueWrapper.Create(Color));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Croak";
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