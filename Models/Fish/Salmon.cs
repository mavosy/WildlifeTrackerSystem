using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Fish
{
    public class Salmon : Fish
    {
        private FoodSchedule _foodSchedule;

        public Salmon(string id, string? name, int? age, GenderType gender, WaterHabitatType habitat, bool hasBeenCaught)
            : base(id, name, age, gender, habitat)
        {
            HasBeenCaught = hasBeenCaught;
            SetFoodSchedule();
        }

        public bool HasBeenCaught { get; set; }
        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("HasBeenCaught", ValueWrapper.Create(HasBeenCaught));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Splash";
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