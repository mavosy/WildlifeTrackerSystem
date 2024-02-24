using WTS.Enums;

namespace WTS.Models.Fish
{
    internal class Shark(string id, int age, GenderType gender, string name, WaterHabitatType habitat, int numberOfGills) : Fish(id, age, gender, name, habitat)
    {
        private int _numberOfGills = numberOfGills;
        public int HasBeenCaught
        {
            get { return _numberOfGills; }
            set { _numberOfGills = value; }
        }
    }
}
