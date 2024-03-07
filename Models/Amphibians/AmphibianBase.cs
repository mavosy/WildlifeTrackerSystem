using WTS.Enums;
using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.Models.Amphibians
{
    internal abstract class Amphibian : Animal
    {
        protected Amphibian(string id, string? name, int? age, GenderType gender, bool landliving)
            : base(id, CategoryType.Amphibian, gender, name, age)
        {
            Landliving = landliving;
        }

        protected bool Landliving { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("LandLiving", ValueWrapper.Create(Landliving));
        }
    }
}