using System.ComponentModel.DataAnnotations;
using WTS.Enums;
using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.Models.Fish
{
    internal abstract class Fish : Animal
    {
        protected Fish(string id, string? name, int? age, GenderType gender, WaterHabitatType habitat = WaterHabitatType.Unknown)
            : base(id, CategoryType.Fish, gender, name, age)
        {
            Habitat = habitat;
        }

        [Required(ErrorMessage = "This information is required")]
        protected WaterHabitatType Habitat { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Habitat", ValueWrapper.Create(Habitat));
        }
    }
}
