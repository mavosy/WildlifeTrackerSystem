using WTS.Enums;

namespace WTS.Models.Reptiles
{
    internal class Tortoise(string id, int age, GenderType gender, string name, bool hasScales, int maxAgeInYears) : Reptile(id, age, gender, name, hasScales)
    {
        private int _maxAgeInYears = maxAgeInYears;

        public int HuntingTechnique
        {
            get { return _maxAgeInYears; }
            set { _maxAgeInYears = value; }
        }
    }
}