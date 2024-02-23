using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Mammals
{
    internal abstract class Mammal : Animal
    {
        private int _numberOfLegs;

        public int NumberOFLegs
        {
            get { return _numberOfLegs; }
            set { _numberOfLegs = value; }
        }

        protected Mammal(string name, string id, int age, GenderType gender, int numberOfLegs)
            : base(id, age, CategoryType.Mammal, gender, name)
        {
            _numberOfLegs = numberOfLegs;
        }
    }
}
