using System.ComponentModel.DataAnnotations;
using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Reptiles
{
    internal class Tortoise : Reptile
    {
        public Tortoise(string id, string? name, int? age, GenderType gender, bool hasScales, int maxAgeInYears) 
            : base(id, name, age, gender, hasScales)
        {
            MaxAgeInYears = maxAgeInYears;
        }

        [Required(ErrorMessage = "This information is required")]
        [Range(0, 200, ErrorMessage = "Must be between 0 and 200")]
        public int MaxAgeInYears { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("MaxAgeInYears", ValueWrapper.Create(MaxAgeInYears));
        }
    }
}