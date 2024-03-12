using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Birds
{
    public class Raven : Bird
    {
        private FoodSchedule _foodSchedule;

        public Raven(string id, string? name, int? age, GenderType gender, bool migratory, bool hasHatchling)
            : base(id, name, age, gender, migratory)
        {
            HasHatchling = hasHatchling;
            SetFoodSchedule();
        }

        public bool HasHatchling { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("HasHatchling", ValueWrapper.Create(HasHatchling));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Caw";
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