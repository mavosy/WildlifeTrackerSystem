using WTS.Enums;

namespace WTS.Models.Insects
{
    internal class Bee(string id, int age, GenderType gender, string name, bool canFly, bool solitary) : Insect(id, age, gender, name, canFly)
    {
        private bool _solitary = solitary;
        public bool Solitary
        {
            get { return _solitary; }
            set { _solitary = value; }
        }
    }
}