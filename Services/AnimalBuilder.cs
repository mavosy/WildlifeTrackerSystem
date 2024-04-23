using WTS.Configurations;
using WTS.Models.Amphibians;
using WTS.Models.AnimalBase;
using WTS.Models.Arachnids;
using WTS.Models.Birds;
using WTS.Models.Fish;
using WTS.Models.Insects;
using WTS.Models.Mammals;
using WTS.Models.Reptiles;

namespace WTS.Services
{
    /// <summary>
    /// Beginning of refactoring of Animal object creation to a builder
    /// </summary>
    public class AnimalBuilder
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

        private readonly Dictionary<string, Func<AnimalConfig, Animal>> _speciesCreationMap;
        private readonly Dictionary<string, Action<Animal>> _propertySettersMap;

        public AnimalBuilder()
        {
            _speciesCreationMap = InitializeSpeciesCreationMap();
            _propertySettersMap = new Dictionary<string, Action<Animal>>();
        }
        public AnimalBuilder SetProperty(string propertyName, Action<Animal> setter)
        {
            _propertySettersMap[propertyName] = setter;
            return this;
        }

        public Animal BuildAnimal(string speciesType, AnimalConfig config)
        {
            if (_speciesCreationMap.TryGetValue(speciesType, out var createAnimalFunc))
            {
                return createAnimalFunc(config);
            }
            throw new ArgumentException($"Unsupported species type: {speciesType}");
        }

        /// <summary>
        /// Initializes the mapping between species and their creation functions.
        /// </summary>
        private Dictionary<string, Func<AnimalConfig, Animal>> InitializeSpeciesCreationMap()
        {
            return new Dictionary<string, Func<AnimalConfig, Animal>>
            {
                [Frog] = config => new Frog(config.Id, config.Name, config.Age, config.SelectedGender, config.Landliving, config.Color),
                [Axolotl] = config => new Axolotl(config.Id, config.Name, config.Age, config.SelectedGender, config.Landliving, config.RegenerationRate),
                [Spider] = config => new Spider(config.Id, config.Name, config.Age, config.SelectedGender, config.Venomous, config.WebWeaving),
                [Scorpion] = config => new Scorpion(config.Id, config.Name, config.Age, config.SelectedGender, config.Venomous, config.Nocturnal),
                [Raven] = config => new Raven(config.Id, config.Name, config.Age, config.SelectedGender, config.Migratory, config.HasHatchling),
                [Falcon] = config => new Falcon(config.Id, config.Name, config.Age, config.SelectedGender, config.Migratory, config.DivingSpeed),
                [Salmon] = config => new Salmon(config.Id, config.Name, config.Age, config.SelectedGender, config.SelectedWaterType, config.HasBeenCaught),
                [Shark] = config => new Shark(config.Id, config.Name, config.Age, config.SelectedGender, config.SelectedWaterType, config.NumberOfGills),
                [Bee] = config => new Bee(config.Id, config.Name, config.Age, config.SelectedGender, config.CanFly, config.Solitary),
                [Ladybug] = config => new Ladybug(config.Id, config.Name, config.Age, config.SelectedGender, config.CanFly, config.NumberOfSpots),
                [Cat] = config => new Cat(config.Id, config.Name, config.Age, config.SelectedGender, config.NumberOfLegs, config.Breed),
                [Elephant] = config => new Elephant(config.Id, config.Name, config.Age, config.SelectedGender, config.NumberOfLegs, config.TrunkLength),
                [Snake] = config => new Snake(config.Id, config.Name, config.Age, config.SelectedGender, config.HasScales, config.SelectedHuntingTechnique),
                [Tortoise] = config => new Tortoise(config.Id, config.Name, config.Age, config.SelectedGender, config.HasScales, config.MaxAgeInYears),
            };
        }
    }
}