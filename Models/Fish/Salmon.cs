using WTS.Enums;

namespace WTS.Models.Fish
{
    internal class Salmon : Fish
    {
        public Salmon(string id, string? name, int? age, GenderType gender, WaterHabitatType habitat, bool hasBeenCaught) 
            : base(id, name, age, gender, habitat)
        {
            HasBeenCaught = hasBeenCaught;
        }

        public bool HasBeenCaught { get; set; }
        public override string ToString()
        {
            return $"{base.ToString()}, Has been caught: {HasBeenCaught}";
        }

    }
}