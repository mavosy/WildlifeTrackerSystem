using WTS.Enums;

namespace WTS.Models.Reptiles
{
    internal class Snake : Reptile
    {
        public Snake(string id, string? name, int? age, GenderType gender, bool hasScales, HuntingTechniqueType huntingTechnique) 
            : base(id, name, age, gender, hasScales)
        {
            HuntingTechnique = huntingTechnique;
        }

        public HuntingTechniqueType HuntingTechnique { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Hunting technique: {HuntingTechnique}";
        }
    }
}