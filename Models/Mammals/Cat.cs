using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Enums;

namespace WTS.Models.Mammals
{
    internal class Cat : Mammal
    {
        private string _breed;

        public string Breed
        {
            get { return _breed; }
            set { _breed = value; }
        }

        public Cat(int id, int age,  GenderType gender, string name, int numberOfLegs, string breed)
            : base(name, id, age, gender, numberOfLegs)
        {
            _breed = breed;
        }
    }
}