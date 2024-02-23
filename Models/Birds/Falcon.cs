using WTS.Enums;

namespace WTS.Models.Birds
{
    internal class Falcon(string id, int age, GenderType gender, string name, bool migratory, int divingSpeed) : Bird(id, age, gender, name, migratory)
    {
        private int _divingSpeed = divingSpeed;
        public int DivingSpeed
        {
            get { return _divingSpeed; }
            set { _divingSpeed = value; }
        }
    }
}
