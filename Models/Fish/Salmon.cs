using WTS.Enums;

namespace WTS.Models.Fish
{
    internal class Salmon(string id, int age, GenderType gender, string name, WaterHabitatType habitat, bool numberOfGills) : Fish(id, age, gender, name, habitat)
    {
        private bool _hasBeenCaught = numberOfGills;
        public bool NumberOfGills
        {
            get { return _hasBeenCaught; }
            set { _hasBeenCaught = value; }
        }
    }
}