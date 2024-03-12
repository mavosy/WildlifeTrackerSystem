using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Reptiles
{
    public class Snake : Reptile
    {
        private FoodSchedule _foodSchedule;

        public Snake(string id, string? name, int? age, GenderType gender, bool hasScales, HuntingTechniqueType huntingTechnique = HuntingTechniqueType.Unknown)
            : base(id, name, age, gender, hasScales)
        {
            HuntingTechnique = huntingTechnique;
            SetFoodSchedule();
        }

        public HuntingTechniqueType HuntingTechnique { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("SelectedHuntingTechnique", ValueWrapper.Create(HuntingTechnique));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Hiss";
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