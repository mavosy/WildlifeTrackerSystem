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

        private void SetFoodSchedule()
        {
            _foodSchedule = new FoodSchedule();
            _foodSchedule.EaterType = EaterType.Carnivore;
            _foodSchedule.Add("Morning: Flakes and milk");
            _foodSchedule.Add("Lunch:  Bones and flakes");
            _foodSchedule.Add("Evening: Any meat dish.");
        }

        public override FoodSchedule GetFoodSchedule()
        {
            return _foodSchedule;
        }
    }
}