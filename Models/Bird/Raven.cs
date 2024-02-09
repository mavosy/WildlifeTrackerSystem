using WTS.Enums;

namespace WTS.Models.Birds
{
    internal class Raven : Bird
    {
        private bool _hasHatchling;

        public bool HasHatchling
        {
            get { return _hasHatchling; }
            set { _hasHatchling = value; }
        }

        public Raven(int id, int age, GenderType gender, string name, bool isMigratory, bool keenEyesight)
            : base(id, age, gender, name, isMigratory)
        {
            _hasHatchling = keenEyesight;
        }
    }

}