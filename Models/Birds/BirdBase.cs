using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Birds
{
    internal abstract class Bird : Animal
    {
        protected Bird(string id, string? name, int? age, GenderType gender, bool migratory)
            : base(id, CategoryType.Bird, gender, name, age)
        {
            Migratory = migratory;
        }

        protected bool Migratory { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Migratory: {Migratory}";
        }
    }
}