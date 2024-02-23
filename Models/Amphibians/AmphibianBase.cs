using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Amphibians
{
    internal abstract class Amphibian : Animal
    {
        private bool _landliving;

        protected bool Landliving
        {
            get { return _landliving; }
            set { _landliving = value; }
        }

        protected Amphibian(string id, int age, GenderType gender, string name, bool landliving)
            : base(id, age, CategoryType.Amphibian, gender, name)
        {
            _landliving = landliving;
        }
    }
}