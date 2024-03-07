using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Insects
{
    internal class Bee : Insect
    {
        public Bee(string id, string? name, int? age, GenderType gender, bool canFly, bool solitary) 
            : base(id, name, age, gender, canFly)
        {
            Solitary = solitary;
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
    }
}