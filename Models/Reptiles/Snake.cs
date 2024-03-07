using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Reptiles
{
    public class Snake : Reptile
    {
        public Snake(string id, string? name, int? age, GenderType gender, bool hasScales, HuntingTechniqueType huntingTechnique = HuntingTechniqueType.Unknown) 
            : base(id, name, age, gender, hasScales)
        {
            HuntingTechnique = huntingTechnique;
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
    }
}