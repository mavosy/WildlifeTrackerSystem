using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Enums;
using WTS.Models.Amphibians;
using WTS.Models.Arachnids;
using WTS.Models.Birds;
using WTS.Models.Fish;
using WTS.Models.Insects;
using WTS.Models.Mammals;
using WTS.Models.Reptiles;

namespace WTS.Services
{
    internal class VisibilityService
    {
        private const string Frog = "Frog";
        private const string Axolotl = "Axolotl";
        private const string Spider = "Spider";
        private const string Scorpion = "Scorpion";
        private const string Raven = "Raven";
        private const string Falcon = "Falcon";
        private const string Salmon = "Salmon";
        private const string Shark = "Shark";
        private const string Bee = "Bee";
        private const string Ladybug = "Ladybug";
        private const string Cat = "Cat";
        private const string Elephant = "Elephant";
        private const string Snake = "Snake";
        private const string Tortoise = "Tortoise";

        private Dictionary<CategoryType, Action> _categoryVisibilityMap;
        private Dictionary<string, Action> _speciesVisibilityMap;

        public VisibilityService()
        {
            _categoryVisibilityMap = InitializeCategoryVisibilityMap();
            _speciesVisibilityMap = InitializeSpeciesVisibilityMap();
        }

        //Booleans for UI visibility collapse
        public bool LandlivingVisible { get; set; } = false;
        public bool ColorVisible { get; set; } = false;
        public bool RegenerationRateVisible { get; set; } = false;
        public bool VenomousVisible { get; set; } = false;
        public bool WebWeavingVisible { get; set; } = false;
        public bool NocturnalVisible { get; set; } = false;
        public bool MigratoryVisible { get; set; } = false;
        public bool HasHatchlingVisible { get; set; } = false;
        public bool DivingSpeedVisible { get; set; } = false;
        public bool WaterTypeVisible { get; set; } = false;
        public bool HasBeenCaughtVisible { get; set; } = false;
        public bool NumberOfGillsVisible { get; set; } = false;
        public bool CanFlyVisible { get; set; } = false;
        public bool SolitaryVisible { get; set; } = false;
        public bool NumberOfSpotsVisible { get; set; } = false;
        public bool NumberOfLegsVisible { get; set; } = false;
        public bool BreedVisible { get; set; } = false;
        public bool TrunkLengthVisible { get; set; } = false;
        public bool HasScalesVisible { get; set; } = false;
        public bool HuntingTechniqueVisible { get; set; } = false;
        public bool MaxAgeInYearsVisible { get; set; } = false;

        /// <summary>
        /// Updates the visibility of the input fields related to the currently selected species.
        /// </summary>
        public void UpdateSpeciesInputVisibility(string selectedSpecies)
        {
            ResetSpeciesInputVisibility();

            if (selectedSpecies is not null && _speciesVisibilityMap.TryGetValue(selectedSpecies, out Action? setVisibility))
            {
                setVisibility?.Invoke();
            }
        }

        /// <summary>
        /// Updates the visibility of the input fields related to the currently selected category.
        /// </summary>

        public void UpdateCategoryInputVisibility(CategoryType selectedCategory)
        {
            ResetCategoryInputVisibility();

            if (_categoryVisibilityMap.TryGetValue(selectedCategory, out Action setVisibility))
            {
                setVisibility.Invoke();
            }
        }

        /// <summary>
        /// Resets the visibility of all category-related input fields to their default state.
        /// </summary>
        private void ResetCategoryInputVisibility()
        {
            LandlivingVisible = false;
            VenomousVisible = false;
            MigratoryVisible = false;
            WaterTypeVisible = false;
            CanFlyVisible = false;
            NumberOfLegsVisible = false;
            HasScalesVisible = false;
        }

        /// <summary>
        /// Resets the visibility of all species-related input fields to their default state.
        /// </summary>
        private void ResetSpeciesInputVisibility()
        {
            ColorVisible = false;
            RegenerationRateVisible = false;
            WebWeavingVisible = false;
            NocturnalVisible = false;
            HasHatchlingVisible = false;
            DivingSpeedVisible = false;
            HasBeenCaughtVisible = false;
            NumberOfGillsVisible = false;
            SolitaryVisible = false;
            NumberOfSpotsVisible = false;
            BreedVisible = false;
            TrunkLengthVisible = false;
            HuntingTechniqueVisible = false;
            MaxAgeInYearsVisible = false;
        }

        private Dictionary<CategoryType, Action> InitializeCategoryVisibilityMap()
        {
            return new Dictionary<CategoryType, Action>
            {
                { CategoryType.Amphibian, () => LandlivingVisible = true },
                { CategoryType.Arachnid, () => VenomousVisible = true },
                { CategoryType.Bird, () => MigratoryVisible = true },
                { CategoryType.Fish, () => WaterTypeVisible = true },
                { CategoryType.Insect, () => CanFlyVisible = true },
                { CategoryType.Mammal, () => NumberOfLegsVisible = true },
                { CategoryType.Reptile, () => HasScalesVisible = true },
            };
        }

        /// <summary>
        /// Initializes the mapping between species and their visibility actions.
        /// </summary>
        private Dictionary<string, Action> InitializeSpeciesVisibilityMap()
        {
            return new Dictionary<string, Action>
            {
                { Frog, () => ColorVisible = true },
                { Axolotl, () => RegenerationRateVisible = true },
                { Spider, () => WebWeavingVisible = true },
                { Scorpion, () => NocturnalVisible = true },
                { Raven, () => HasHatchlingVisible = true },
                { Falcon, () => DivingSpeedVisible = true },
                { Salmon, () => HasBeenCaughtVisible = true },
                { Shark, () => NumberOfGillsVisible = true },
                { Bee, () => SolitaryVisible = true },
                { Ladybug, () => NumberOfSpotsVisible = true },
                { Cat, () => BreedVisible = true },
                { Elephant, () => TrunkLengthVisible = true },
                { Snake, () => HuntingTechniqueVisible = true },
                { Tortoise, () => MaxAgeInYearsVisible = true },
            };
        }
    }
}
