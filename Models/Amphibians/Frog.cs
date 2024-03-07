using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Amphibians
{
    internal class Frog : Amphibian
    {
        public Frog(string id, string? name, int? age, GenderType gender, bool landliving, string color)
            : base(id, name, age, gender, landliving)
        {
            Color = color;
        }

        public string Color { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Color", ValueWrapper.Create(Color));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Croak";
        }
    }
}