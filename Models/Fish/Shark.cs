using System.ComponentModel.DataAnnotations;
using WTS.Enums;

namespace WTS.Models.Fish
{
    internal class Shark : Fish
    {
        public Shark(string id, string? name, int? age, GenderType gender, WaterHabitatType habitat, int numberOfGills) 
            : base(id, name, age, gender, habitat)
        {
            NumberOfGills = numberOfGills;
        }

        [Required(ErrorMessage = "This information is required")]
        [Range(0, 7, ErrorMessage = "Must be between 0 and 7")]
        public int NumberOfGills { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Number of gills: {NumberOfGills}";
        }
    }
}