using System.Reflection;
using System.Windows.Controls;
using System.Xml.Linq;
using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Birds
{
    internal abstract class Bird : Animal
    {
        private bool _isMigratory;

        public bool IsMigratory
        {
            get { return _isMigratory; }
            set { _isMigratory = value; }
        }

        public Bird(int id, int age, GenderType gender, string? name, bool isMigratory)
            : base(id, age, CategoryType.Bird, gender, name)
        {
            IsMigratory = isMigratory;
        }
    }
}
