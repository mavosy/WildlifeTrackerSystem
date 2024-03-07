using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Mammals
{
    internal class Elephant : Mammal
    {
        public Elephant(string id, string? name, int? age, GenderType gender, int numberOfLegs, int trunkLength)
            : base(id, name, age, gender, numberOfLegs)
        {
            TrunkLength = trunkLength;
        }

        /// <summary>
        /// Trunk length in centimeters
        /// </summary>
        public int TrunkLength { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("TrunkLength", ValueWrapper.Create(TrunkLength));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Trumpet";
        }
    }
}