using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Amphibians
{
    internal class Axolotl : Amphibian
    {
        public Axolotl(string id, string? name, int? age, GenderType gender, bool landliving, double regenerationRate)
            : base(id, name, age, gender, landliving)
        {
            RegenerationRate = regenerationRate;
        }

        /// <summary>
        /// Regrowth in millimeters per day
        /// </summary>
        public double RegenerationRate { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Regeneration", ValueWrapper.Create(RegenerationRate));
        }
    }
}