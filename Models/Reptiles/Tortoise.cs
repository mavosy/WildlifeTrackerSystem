using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Reptiles
{
    public class Tortoise : Reptile
    {
        private FoodSchedule _foodSchedule;

        public Tortoise(string id, string? name, int? age, GenderType gender, bool hasScales, int maxAgeInYears)
            : base(id, name, age, gender, hasScales)
        {
            MaxAgeInYears = maxAgeInYears;
            SetFoodSchedule();
        }

        public int MaxAgeInYears { get; set; }

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

        private void SetFoodSchedule()
        {
            _foodSchedule = new FoodSchedule();
            _foodSchedule.EaterType = EaterType.Herbivore;
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