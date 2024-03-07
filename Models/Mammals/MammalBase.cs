using System.ComponentModel.DataAnnotations;
using WTS.Enums;
using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.Models.Mammals
{
    internal abstract class Mammal : Animal
    {
        protected Mammal(string id, string? name, int? age, GenderType gender, int numberOfLegs)
            : base(id, CategoryType.Mammal, gender, name, age)
        {
            NumberOfLegs = numberOfLegs;
        }

        [Required(ErrorMessage = "This information is required")]
        [Range(0, 4, ErrorMessage = "Must be between 0 and 4")]
        protected int NumberOfLegs { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("NumberOfLegs", ValueWrapper.Create(NumberOfLegs));
        }
    }
}