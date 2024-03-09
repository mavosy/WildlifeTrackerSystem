using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Fish
{
    public class Shark : Fish
    {
        public Shark(string id, string? name, int? age, GenderType gender, WaterHabitatType habitat, int numberOfGills)
            : base(id, name, age, gender, habitat)
        {
            NumberOfGills = numberOfGills;
            SetFoodSchedule();
        }

        public int NumberOfGills { get; set; }

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

        private FoodSchedule _foodSchedule;
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