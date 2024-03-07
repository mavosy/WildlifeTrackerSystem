using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "This information is required")]
        [Range(0, 24, ErrorMessage = "Must be between 0 and 24")]
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