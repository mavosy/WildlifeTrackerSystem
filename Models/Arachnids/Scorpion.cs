using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Arachnids
{
    public class Scorpion : Arachnid
    {
        private FoodSchedule _foodSchedule;

        public Scorpion(string id, string? name, int? age, GenderType gender, bool venomous, bool nocturnal)
            : base(id, name, age, gender, venomous)
        {
            Nocturnal = nocturnal;
            SetFoodSchedule();
        }

        public bool Nocturnal { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Nocturnal", ValueWrapper.Create(Nocturnal));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Click";
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