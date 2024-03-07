using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Fish
{
    internal class Shark : Fish
    {
        public Shark(string id, string? name, int? age, GenderType gender, WaterHabitatType habitat, int numberOfGills)
            : base(id, name, age, gender, habitat)
        {
            NumberOfGills = numberOfGills;
        }

        public int NumberOfGills { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("NumberOfGills", ValueWrapper.Create(NumberOfGills));
        }
    }
}