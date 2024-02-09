using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Enums;

namespace WTS.Models.Insects
{
    internal class Bee : Insect
    {
        private bool _isSolitary;

        public bool IsSolitary
        {
            get { return _isSolitary; }
            set { _isSolitary = value; }
        }

        public Bee(int id, int age, GenderType gender, string name, bool canFly, bool isSolitary)
            : base(id, age, gender, name, canFly)
        {
            _isSolitary = isSolitary;
        }
    }
}
