using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Arachnids
{
    internal class Scorpion : Arachnid
    {
        public Scorpion(string id, string? name, int? age, GenderType gender, bool venomous, bool nocturnal)
            : base(id, name, age, gender, venomous)
        {
            Nocturnal = nocturnal;
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
    }
}