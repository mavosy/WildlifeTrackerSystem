using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Birds
{
    public class Falcon : Bird
    {
        private FoodSchedule _foodSchedule;

        public Falcon(string id, string? name, int? age, GenderType gender, bool migratory, int? divingSpeed)
            : base(id, name, age, gender, migratory)
        {
            DivingSpeed = divingSpeed;
            SetFoodSchedule();
        }

        /// <summary>
        /// Top speed while diving, in km/h
        /// </summary>
        public int? DivingSpeed { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("DivingSpeed", ValueWrapper.Create(DivingSpeed));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Screech";
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