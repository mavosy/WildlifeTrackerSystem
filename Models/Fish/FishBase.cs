using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Fish
{
    internal abstract class Fish : Animal
    {
        private WaterHabitatType _habitat;

        public WaterHabitatType Habitat
        {
            get { return _habitat; }
            set { _habitat = value; }
        }

        protected Fish(int id, int age, GenderType gender, string name, WaterHabitatType habitat)
            : base(id, age, CategoryType.Fish, gender, name)
        {
            _habitat = habitat;
        }
    }
}
