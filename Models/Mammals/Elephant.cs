using System.ComponentModel.DataAnnotations;
using WTS.Enums;

namespace WTS.Models.Mammals
{
    internal class Elephant : Mammal
    {
        public Elephant(string id, string? name, int? age, GenderType gender, int numberOfLegs, int trunkLength) 
            : base(id, name, age, gender, numberOfLegs)
        {
            TrunkLength = trunkLength;
        }

        /// <summary>
        /// Trunk length in centimeters
        /// </summary>
        [Required(ErrorMessage = "This information is required")]
        [Range(0, 250, ErrorMessage = "Must be between 0 and 250")]
        public int TrunkLength { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Trunk length: {TrunkLength}";
        }
    }
}