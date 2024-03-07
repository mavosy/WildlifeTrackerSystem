using WTS.Enums;
using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.Models.Insects
{
    internal abstract class Insect : Animal
    {
        protected Insect(string id, string? name, int? age, GenderType gender,  bool canFly)
            : base(id, CategoryType.Insect, gender, name, age)
        {
            CanFly = canFly;
        }

        protected bool CanFly { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("CanFly", ValueWrapper.Create(CanFly));
        }
    }
}