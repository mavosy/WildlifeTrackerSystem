using WTS.Enums;

namespace WTS.Models.Mammals
{
    internal class Cat(string id, int age, GenderType gender, string name, int numberOfLegs, string breed) : Mammal(name, id, age, gender, numberOfLegs)
    {
        private string _breed = breed;
        public string Breed
        {
            get { return _breed; }
            set { _breed = value; }
        }
    }
}