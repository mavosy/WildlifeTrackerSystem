using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Enums;

namespace WTS.Models.Fish
{
    internal class Salmon : Fish
    {
        private bool _hasBeenCaught;

        public bool HasBeenCaught
        {
            get { return _hasBeenCaught; }
            set { _hasBeenCaught = value; }
        }

        public Salmon(int id, int age, GenderType gender, string name, WaterHabitatType habitat, bool hasBeenCaught)
            : base(id, age, gender, name, habitat)
        {
            _hasBeenCaught = hasBeenCaught;
        }
    }
}
