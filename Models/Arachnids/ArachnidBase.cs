using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Arachnids
{
    internal abstract class Arachnid : Animal
    {
        protected Arachnid(string id, string? name, int? age, GenderType gender, bool venomous)
            : base(id, CategoryType.Arachnid, gender, name, age)
        {
            Venomous = venomous;
        }

        protected bool Venomous { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Venomous: {Venomous}";
        }
    }
}