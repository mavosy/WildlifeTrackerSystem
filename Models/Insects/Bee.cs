using WTS.Enums;

namespace WTS.Models.Insects
{
    internal class Bee : Insect
    {
        public Bee(string id, string? name, int? age, GenderType gender, bool canFly, bool solitary) 
            : base(id, name, age, gender, canFly)
        {
            Solitary = solitary;
        }

        public bool Solitary { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Solitary: {Solitary}";
        }
    }
}