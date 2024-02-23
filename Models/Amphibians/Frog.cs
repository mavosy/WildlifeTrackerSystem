using WTS.Enums;

namespace WTS.Models.Amphibians
{
    internal class Frog(string id, int age, GenderType gender, string name, bool landliving, string color) : Amphibian(id, age, gender, name, landliving)
    {
        private string _color = color;
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
    }
}
