using WTS.Enums;
using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.Models.Reptiles
{
    internal abstract class Reptile : Animal
    {
        protected Reptile(string id, string? name, int? age, GenderType gender, bool hasScales)
            : base(id, CategoryType.Reptile, gender, name, age)
        {
            HasScales = hasScales;
        }

        protected bool HasScales { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("HasScales", ValueWrapper.Create(HasScales));
        }
    }
}