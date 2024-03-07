using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Fish
{
    public class Salmon : Fish
    {
        public Salmon(string id, string? name, int? age, GenderType gender, WaterHabitatType habitat, bool hasBeenCaught) 
            : base(id, name, age, gender, habitat)
        {
            HasBeenCaught = hasBeenCaught;
        }

        public bool HasBeenCaught { get; set; }
        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("HasBeenCaught", ValueWrapper.Create(HasBeenCaught));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Splash";
        }
    }
}