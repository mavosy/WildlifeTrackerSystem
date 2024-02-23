using WTS.Enums;

namespace WTS.Models.Arachnids
{
    internal class Spider(string id, int age, GenderType gender, string name, bool venomous, bool webWeaving) : Arachnid(id, age, gender, name, venomous)
    {
        private bool _webWeaving = webWeaving;
        public bool WebWeaving
        {
            get { return _webWeaving; }
            set { _webWeaving = value; }
        }
    }
}