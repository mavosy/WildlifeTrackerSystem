using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Birds
{
    public class Raven : Bird
    {
        public Raven(string id, string? name, int? age, GenderType gender, bool migratory, bool hasHatchling) 
            : base(id, name, age, gender, migratory)
        {
            HasHatchling = hasHatchling;
        }

        public bool HasHatchling { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("HasHatchling", ValueWrapper.Create(HasHatchling));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Caw";
        }
    }
}