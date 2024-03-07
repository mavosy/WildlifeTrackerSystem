using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Insects
{
    internal class Ladybug : Insect
    {
        public Ladybug(string id, string? name, int? age, GenderType gender, bool canFly, int numberOfSpots)
            : base(id, name, age, gender, canFly)
        {
            NumberOfSpots = numberOfSpots;
        }

        public int NumberOfSpots { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("NumberOfSpots", ValueWrapper.Create(NumberOfSpots));
        }
    }
}