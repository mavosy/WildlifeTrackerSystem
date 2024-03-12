using WTS.Enums;

namespace WTS.Configurations
{
    /// <summary>
    /// Configuration for Animal Builder
    /// </summary>
    public class AnimalConfig
    {
        // Animal
        public string Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }

        public GenderType SelectedGender { get; set; }

        // Amphibian
        public bool Landliving { get; set; }
        public string Color { get; set; }
        public double RegenerationRate { get; set; }

        // Arachnid
        public bool Venomous { get; set; }
        public bool WebWeaving { get; set; }
        public bool Nocturnal { get; set; }

        // Bird
        public bool Migratory { get; set; }
        public bool HasHatchling { get; set; }
        public int DivingSpeed { get; set; }

        // Fish
        public WaterHabitatType SelectedWaterType { get; set; }
        public bool HasBeenCaught { get; set; }
        public int NumberOfGills { get; set; }

        // Insect
        public bool CanFly { get; set; }
        public bool Solitary { get; set; }
        public int NumberOfSpots { get; set; }

        // Mammal
        public int NumberOfLegs { get; set; }
        public string Breed { get; set; }
        public int TrunkLength { get; set; }

        // Reptile
        public bool HasScales { get; set; }
        public HuntingTechniqueType SelectedHuntingTechnique { get; set; }
        public int MaxAgeInYears { get; set; }
    }
}