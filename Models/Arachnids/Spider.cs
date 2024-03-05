using WTS.Enums;

namespace WTS.Models.Arachnids
{
    internal class Spider : Arachnid
    {
        public Spider(string id, string? name, int? age, GenderType gender, bool venomous, bool webWeaving) 
            : base(id, name, age, gender, venomous)
        {
            WebWeaving = webWeaving;
        }

        public bool WebWeaving { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Web weaving: {WebWeaving}";
        }
    }
}