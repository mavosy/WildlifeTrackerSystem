using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Birds
{
    internal abstract class Bird : Animal
    {
        private bool _migratory;

        public bool Migratory
        {
            get { return _migratory; }
            set { _migratory = value; }
        }

        public Bird(string id, int age, GenderType gender, string? name, bool migratory)
            : base(id, age, CategoryType.Bird, gender, name)
        {
            Migratory = migratory;
        }
    }
}
