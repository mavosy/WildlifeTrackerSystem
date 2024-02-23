using WTS.Enums;

namespace WTS.Models.Reptiles
{
    internal class Snake(string id, int age, GenderType gender, string name, bool hasScales, HuntingTechniqueType huntingTechnique) : Reptile(id, age, gender, name, hasScales)
    {
        private HuntingTechniqueType _huntingTechnique = huntingTechnique;

        public HuntingTechniqueType HuntingTechnique
        {
            get { return _huntingTechnique; }
            set { _huntingTechnique = value; }
        }
    }
}