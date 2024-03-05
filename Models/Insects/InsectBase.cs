using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Insects
{
    internal abstract class Insect : Animal
    {
        protected Insect(string id, string? name, int? age, GenderType gender,  bool canFly)
            : base(id, CategoryType.Insect, gender, name, age)
        {
            CanFly = canFly;
        }

        protected bool CanFly { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Can fly: {CanFly}";
        }
    }
}