using WTS.Enums;

namespace WTS.Models.Birds
{
    internal class Raven : Bird
    {
        public Raven(string id, string? name, int? age, GenderType gender, bool migratory, bool hasHatchling) 
            : base(id, name, age, gender, migratory)
        {
            HasHatchling = hasHatchling;
        }

        public bool HasHatchling { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Has hatchling: {HasHatchling}";
        }
    }
}