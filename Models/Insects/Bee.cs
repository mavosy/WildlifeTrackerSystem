using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Insects
{
    public class Bee : Insect
    {
        private FoodSchedule _foodSchedule;

        public Bee(string id, string? name, int? age, GenderType gender, bool canFly, bool solitary)
            : base(id, name, age, gender, canFly)
        {
            Solitary = solitary;
            SetFoodSchedule();
        }

        public bool Solitary { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Solitary", ValueWrapper.Create(Solitary));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Buzz";
        }

        /// <summary>
        /// Sets the food schedule and food consumption category for the animal.
        /// </summary>
        private void SetFoodSchedule()
        {
            _foodSchedule = new FoodSchedule();
            _foodSchedule.EaterType = EaterType.Herbivore;
            _foodSchedule.InitializeSchedule();
        }

        public override FoodSchedule GetFoodSchedule()
        {
            return _foodSchedule;
        }
    }
}