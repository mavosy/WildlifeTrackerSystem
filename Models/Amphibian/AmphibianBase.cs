using WTS.Enums;
using WTS.Models.AnimalBase;

namespace WTS.Models.Amphibian
{
    internal abstract class Amphibian : Animal
    {
        private bool _canLiveOnLand;

        protected bool CanLiveOnLand
        {
            get { return _canLiveOnLand; }
            set { _canLiveOnLand = value; }
        }

        protected Amphibian(int id, int age, GenderType gender, string name, bool canLiveOnLand)
            : base(id, age, CategoryType.Amphibian, gender, name)
        {
            _canLiveOnLand = canLiveOnLand;
        }
    } 
}