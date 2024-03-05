using System.ComponentModel.DataAnnotations;
using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Fish
{
    internal abstract class Fish : Animal
    {
        protected Fish(string id, string? name, int? age, GenderType gender, WaterHabitatType habitat)
            : base(id, CategoryType.Fish, gender, name, age)
        {
            Habitat = habitat;
        }

        [Required(ErrorMessage = "This information is required")]
        protected WaterHabitatType Habitat { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Habitat: {Habitat}";
        }
    }
}
