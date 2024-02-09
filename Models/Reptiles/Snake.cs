using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WTS.Models.Reptiles.ReptileBase;
using WTS.Enums;

namespace WTS.Models.Reptiles
{
    internal class Snake : Reptile
    {
        private HuntingTechniqueType _huntingTechnique;

        public HuntingTechniqueType HuntingTechnique
        {
            get { return _huntingTechnique; }
            set { _huntingTechnique = value; }
        }

        public Snake(int id, int age, GenderType gender, string name, bool hasScales, HuntingTechniqueType huntingTechnique)
            : base(id, age, gender, name, hasScales)
        {
            _huntingTechnique = huntingTechnique;
        }
    }

}
