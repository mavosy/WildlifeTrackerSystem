using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Arachnids
{
    public class Spider : Arachnid
    {
        private FoodSchedule _foodSchedule;

        public Spider(string id, string? name, int? age, GenderType gender, bool venomous, bool webWeaving)
            : base(id, name, age, gender, venomous)
        {
            WebWeaving = webWeaving;
            SetFoodSchedule();
        }

        public bool WebWeaving { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("WebWeaving", ValueWrapper.Create(WebWeaving));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Skitter";
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