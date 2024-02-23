using WTS.Enums;

namespace WTS.Models.Arachnids
{
    internal class Scorpion(string id, int age, GenderType gender, string name, bool venomous, bool nocturnal) : Arachnid(id, age, gender, name, venomous)
    {
        private bool _nocturnal = nocturnal;
        public bool Nocturnal
        {
            get { return _nocturnal; }
            set { _nocturnal = value; }
        }
    }
}
