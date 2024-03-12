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