using WTS.Enums;

namespace WTS.Models.Arachnids
{
    internal class Scorpion : Arachnid
    {
        public Scorpion(string id, string? name, int? age, GenderType gender, bool venomous, bool nocturnal) 
            : base(id, name, age, gender, venomous)
        {
            Nocturnal = nocturnal;
        }

        public bool Nocturnal { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Nocturnal: {Nocturnal}";
        }
    }
}