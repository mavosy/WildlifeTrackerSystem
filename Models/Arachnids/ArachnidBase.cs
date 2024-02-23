using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Arachnids
{
    internal abstract class Arachnid : Animal
    {
        private bool _venomous;

        public bool Venomous
        {
            get { return _venomous; }
            set { _venomous = value; }
        }

        protected Arachnid(string id, int age, GenderType gender, string name, bool venomous)
            : base(id, age, CategoryType.Arachnid, gender, name)
        {
            _venomous = venomous;
        }
    }
}