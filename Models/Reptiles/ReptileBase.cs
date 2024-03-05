using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Reptiles
{
    internal abstract class Reptile : Animal
    {
        protected Reptile(string id, string? name, int? age, GenderType gender, bool hasScales)
            : base(id, CategoryType.Reptile, gender, name, age)
        {
            HasScales = hasScales;
        }

        protected bool HasScales { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Has scales: {HasScales}";
        }
    }
}