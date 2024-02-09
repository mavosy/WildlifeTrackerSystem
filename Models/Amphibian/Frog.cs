using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Enums;

namespace WTS.Models.Amphibian
{
    internal class Frog : Amphibian
    {
        private string _color;

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Frog(int id, int age, GenderType gender, string name, bool canLiveOnLand, string color)
            : base(id, age, gender, name, canLiveOnLand)
        {
            _color = color;
        }
    }
}
