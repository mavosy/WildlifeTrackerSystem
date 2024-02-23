using WTS.Enums;

namespace WTS.Models.Mammals
{
    internal class Elephant(string id, int age, GenderType gender, string name, int numberOfLegs, int trunkLength) : Mammal(name, id, age, gender, numberOfLegs)
    {
        private int _trunkLength = trunkLength;
        public int TrunkLength
        {
            get { return _trunkLength; }
            set { _trunkLength = value; }
        }
    }
}