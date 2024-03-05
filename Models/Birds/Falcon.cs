using System.ComponentModel.DataAnnotations;
using WTS.Enums;

namespace WTS.Models.Birds
{
    internal class Falcon : Bird
    {
        public Falcon(string id, string? name, int? age, GenderType gender, bool migratory, int divingSpeed) 
            : base(id, name, age, gender, migratory)
        {
            DivingSpeed = divingSpeed;
        }

        /// <summary>
        /// Top speed while diving, in km/h
        /// </summary>
        [Required(ErrorMessage = "This information is required")]
        [Range(0, 400, ErrorMessage = "Must be between 0 and 400")]
        public int DivingSpeed { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Diving speed: {DivingSpeed}";
        }
    }
}