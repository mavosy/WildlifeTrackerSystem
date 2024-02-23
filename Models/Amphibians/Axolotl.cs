using WTS.Enums;

namespace WTS.Models.Amphibians
{
    internal class Axolotl(string id, int age, GenderType gender, string name, bool landliving, double regenerationRate) : Amphibian(id, age, gender, name, landliving)
    {
        private double _regenerationRate = regenerationRate;
        public double RegenerationRate
        {
            get { return _regenerationRate; }
            set { _regenerationRate = value; }
        }
    }
}
