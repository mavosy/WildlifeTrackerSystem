using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Birds
{
    public class Falcon : Bird
    {
        public Falcon(string id, string? name, int? age, GenderType gender, bool migratory, int divingSpeed)
            : base(id, name, age, gender, migratory)
        {
            DivingSpeed = divingSpeed;
        }

        /// <summary>
        /// Top speed while diving, in km/h
        /// </summary>
        public int DivingSpeed { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("DivingSpeed", ValueWrapper.Create(DivingSpeed));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Screech";
        }
    }
}