using WTS.Enums;
using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.Models.Birds
{
    public abstract class Bird : Animal
    {
        protected Bird(string id, string? name, int? age, GenderType gender, bool migratory)
            : base(id, CategoryType.Bird, gender, name, age)
        {
            Migratory = migratory;
        }

        protected bool Migratory { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Migratory", ValueWrapper.Create(Migratory));
        }
    }
}