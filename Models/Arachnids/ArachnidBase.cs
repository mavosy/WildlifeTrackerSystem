using WTS.Enums;
using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.Models.Arachnids
{
    internal abstract class Arachnid : Animal
    {
        protected Arachnid(string id, string? name, int? age, GenderType gender, bool venomous)
            : base(id, CategoryType.Arachnid, gender, name, age)
        {
            Venomous = venomous;
        }

        protected bool Venomous { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Venomous", ValueWrapper.Create(Venomous));
        }
    }
}