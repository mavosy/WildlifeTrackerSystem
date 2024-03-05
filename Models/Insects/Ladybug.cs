using System.ComponentModel.DataAnnotations;
using WTS.Enums;

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

        public override string ToString()
        {
            return $"{base.ToString()}, Number of spots: {NumberOfSpots}";
        }
    }
}