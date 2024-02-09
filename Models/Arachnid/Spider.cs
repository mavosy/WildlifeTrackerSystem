using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Enums;

namespace WTS.Models.Arachnid
{
    internal class Spider : Arachnid
    {
        private bool _webWeaving;

        public bool WebWeaving
        {
            get { return _webWeaving; }
            set { _webWeaving = value; }
        }

        public Spider(int id, int age, GenderType gender, string name, bool venomous, bool webWeaving)
            : base(id, age, gender, name, venomous)
        {
            _webWeaving = webWeaving;
        }
    }
}
