using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Reptiles
{
    internal class ReptileBase
    {
        internal abstract class Reptile : Animal
        {
            private bool _hasScales;

            public bool HasScales
            {
                get { return _hasScales; }
                set { _hasScales = value; }
            }

            protected Reptile(int id, int age, GenderType gender, string name, bool hasScales)
                : base(id, age, CategoryType.Reptile, gender, name)
            {
                _hasScales = hasScales;
            }
        }
    }
}
