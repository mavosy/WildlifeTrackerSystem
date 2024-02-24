using WTS.Enums;

namespace WTS.Models.Insects
{
    internal class Ladybug(string id, int age, GenderType gender, string name, bool canFly, int numberOfSpots) : Insect(id, age, gender, name, canFly)
    {
        private int _numberOfSpots = numberOfSpots;
        public int NumberOfSpots
        {
            get { return _numberOfSpots; }
            set { _numberOfSpots = value; }
        }
    }
}
