using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Amphibians
{
    public class Axolotl : Amphibian
    {
        private FoodSchedule _foodSchedule;

        public Axolotl(string id, string? name, int? age, GenderType gender, bool landliving, double regenerationRate)
            : base(id, name, age, gender, landliving)
        {
            RegenerationRate = regenerationRate;
            SetFoodSchedule();
        }

        /// <summary>
        /// Regrowth in millimeters per day
        /// </summary>
        public double RegenerationRate { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Regeneration", ValueWrapper.Create(RegenerationRate));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Silent";
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