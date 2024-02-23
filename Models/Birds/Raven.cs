using WTS.Enums;

namespace WTS.Models.Birds
{
    internal class Raven(string id, int age, GenderType gender, string name, bool migratory, bool hasHatchling) : Bird(id, age, gender, name, migratory)
    {
        private bool _hasHatchling = hasHatchling;
        public bool HasHatchling
        {
            get { return _hasHatchling; }
            set { _hasHatchling = value; }
        }
    }
}