using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Arachnids
{
    public class Spider : Arachnid
    {
        public Spider(string id, string? name, int? age, GenderType gender, bool venomous, bool webWeaving) 
            : base(id, name, age, gender, venomous)
        {
            WebWeaving = webWeaving;
        }

        public bool WebWeaving { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("WebWeaving", ValueWrapper.Create(WebWeaving));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Skitter";
        }
    }
}