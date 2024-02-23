using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Insects
{
    internal abstract class Insect : Animal
    {
        private bool _canFly;

        public bool CanFly
        {
            get { return _canFly; }
            set { _canFly = value; }
        }

        protected Insect(string id, int age, GenderType gender, string name, bool canFly)
            : base(id, age, CategoryType.Insect, gender, name)
        {
            _canFly = canFly;
        }
    }
}