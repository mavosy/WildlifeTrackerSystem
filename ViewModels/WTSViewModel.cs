using Microsoft.Win32;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WTS.Commands;
using WTS.Enums;
using WTS.Models;
using WTS.Models.Amphibians;
using WTS.Models.AnimalBase;
using WTS.Models.Arachnids;
using WTS.Models.Birds;
using WTS.Models.Fish;
using WTS.Models.Insects;
using WTS.Models.Mammals;
using WTS.Models.Reptiles;
using WTS.Utilities;

namespace WTS.ViewModels
{
    /// <summary>
    /// Represents the main ViewModel for the application, handling interactions between the View and the Model as part of the MVVM pattern.
    /// </summary>
    class WTSViewModel : BaseViewModel
    {
        // Constants
        private const string ImageFilters = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

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

        // Private fields
        private readonly Dictionary<CategoryType, List<string>> _categoryToSpeciesMap;
        private readonly Dictionary<string, Func<Animal>> _speciesCreationMap;
        private Dictionary<string, Action> _speciesVisibilityMap;

        private CategoryType _selectedCategory;
        private string _selectedSpecies;
        private string _ageText;

        /// <summary>
        /// Constructor of WTSViewModel, initializes a new instance of the WTSViewModel class.
        /// </summary>
        public WTSViewModel()
        {
            _categoryToSpeciesMap = InitializeCategoryToSpeciesMap();
            _speciesCreationMap = InitializeSpeciesCreationMap();
            _speciesVisibilityMap = InitializeSpeciesVisibilityMap();
            AvailableSpecies = new ObservableCollection<string>();
            Animals = new ObservableCollection<Animal>();
            InitializeCommands();
        }

        #region Properties for UI interaction

        /// <summary>
        /// SelectedCategory property represents the currently selected animal category in the UI.
        /// When this property is set, it triggers the update of the AvailableSpecies collection.
        /// </summary>
        public CategoryType SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                UpdateCategoryInputVisibility();

                if (!IsListAllAnimalsChecked)
                {
                    UpdateAvailableSpeciesForCategory(); 
                }
            }
        }

        /// <summary>
        /// SelectedSpecies property represents the currently selected animal species in the UI.
        /// When this property is set, it triggers the update of the visibility of the input fields related to the species.
        /// </summary>
        public string SelectedSpecies
        {
            get { return _selectedSpecies; }
            set
            {
                _selectedSpecies = value;
                UpdateSpeciesInputVisibility();
                SelectedCategory = FindCategoryForSpecies(_selectedSpecies);

            }
        }

        /// <summary>
        /// SelectedAnimal property represents the currently selected animal in the UI.
        /// When this property is set, it triggers the update of the SelectedAnimalInformation property through the fody attribute.
        /// </summary>
        public Animal SelectedAnimal { get; set; }

        /// <summary>
        /// AgeText property represents the text entered in the age input field in the UI.
        /// </summary>
        public string AgeText
        {
            get { return _ageText; }
            set 
            { 
                _ageText = value; 
                if(int.TryParse(value, out int age))
                {
                    Age = age;
                }
            }
        }

        /// <summary>
        /// Rread-only property that provides a detailed description of the currently selected animal.
        /// It depends on the SelectedAnimal property and calls the DisplaySelectedAnimalInfo method to generate the description.
        /// </summary>
        [DependsOn(nameof(SelectedAnimal))]
        public string SelectedAnimalInformation => DisplaySelectedAnimalInfo();
        /// <summary>
        /// AvailableCategories is a read-only property that provides a list of all animal categories that are available for selection.
        /// It retrieves the keys from the _categoryToSpeciesMap dictionary, which maps each category to a list of species.
        /// </summary>
        public IEnumerable<CategoryType> AvailableCategories => _categoryToSpeciesMap.Keys;

        /// <summary>
        /// AvailableSpecies is a property that provides a collection of all animal species that are available for selection in the current category.
        /// The collection is updated whenever the selected category changes.
        /// </summary>
        public ObservableCollection<string> AvailableSpecies { get; private set; }

        /// <summary>
        /// Animals is a property that provides a collection of all animals that have been created.
        /// The collection is updated whenever a new animal is created or an existing animal is edited.
        /// </summary>
        public ObservableCollection<Animal> Animals { get; private set; }

        /// <summary>
        /// CreateAnimalCommand is a property that provides a command for creating a new animal.
        /// The command is bound to the "Create Animal" button in the UI, and it calls the CreateAnimal method when executed.
        /// </summary>
        public ICommand CreateAnimalCommand { get; private set; }

        /// <summary>
        /// ListAllAnimalsCommand is a property that provides a command for listing all animals.
        /// The command is bound to the "List All Animals" button in the UI, and it updates the Animals collection when executed.
        /// </summary>
        public ICommand ListAllAnimalsCommand { get; private set; }

        /// <summary>
        /// AddAnimalImageCommand is a property that provides a command for adding an image to an animal.
        /// The command is bound to the "Add Image" button in the UI, and it calls the OpenFileDialog and FileToBitmapImage methods when executed.
        /// </summary>
        public ICommand AddAnimalImageCommand { get; private set; }

        /// <summary>
        /// AnimalClass is a static read-only property that provides a list of all animal classes defined in the CategoryType enum.
        /// It can be used to populate a dropdown list or other selection control in the UI.
        /// </summary>
        public static IEnumerable<CategoryType> AnimalClass => Enum.GetValues(typeof(CategoryType)).Cast<CategoryType>();

        /// <summary>
        /// Genders is a static read-only property that provides a list of all genders defined in the GenderType enum.
        /// It can be used to populate a dropdown list or other selection control in the UI.
        /// </summary>
        public static IEnumerable<GenderType> Genders => Enum.GetValues(typeof(GenderType)).Cast<GenderType>();

        /// <summary>
        /// WaterTypes is a static read-only property that provides a list of all water habitat types defined in the WaterHabitatType enum.
        /// It can be used to populate a dropdown list or other selection control in the UI.
        /// </summary>
        public static IEnumerable<WaterHabitatType> WaterTypes => Enum.GetValues(typeof(WaterHabitatType)).Cast<WaterHabitatType>();

        /// <summary>
        /// HuntingTechniques is a static read-only property that provides a list of all hunting techniques defined in the HuntingTechniqueType enum.
        /// It can be used to populate a dropdown list or other selection control in the UI, providing the user with a range of hunting techniques to choose from when creating or editing an animal.
        /// </summary>
        public static IEnumerable<HuntingTechniqueType> HuntingTechniques => Enum.GetValues(typeof(HuntingTechniqueType)).Cast<HuntingTechniqueType>();

        /// <summary>
        /// Gets or sets the image in the UI.
        /// </summary>
        public BitmapImage AnimalImage { get; set; }
        #endregion

        /// <summary>
        /// Initializes the commands used in the ViewModel.
        /// </summary>
        private void InitializeCommands()
        {
            CreateAnimalCommand = new RelayCommand(_ => CreateAnimal());
            ListAllAnimalsCommand = new RelayCommand(_ => OnListAllAnimalsCheckedChanged());
            AddAnimalImageCommand = new RelayCommand(_ => DisplayImage());
        }

        #region Visibility for specific category and species properties

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
        public void UpdateSpeciesInputVisibility()
        {
            ResetSpeciesInputVisibility();

            if (_selectedSpecies is not null && _speciesVisibilityMap.TryGetValue(_selectedSpecies, out Action? setVisibility))
            {
                setVisibility?.Invoke();
            }
        }

        /// <summary>
        /// Updates the visibility of the input fields related to the currently selected category.
        /// </summary>
        public void UpdateCategoryInputVisibility()
        {
            ResetCategoryInputVisibility();

            switch (_selectedCategory)
            {
                case CategoryType.Amphibian:
                    LandlivingVisible = true;
                    break;

                case CategoryType.Arachnid:
                    VenomousVisible = true;
                    break;

                case CategoryType.Bird:
                    MigratoryVisible = true;
                    break;

                case CategoryType.Fish:
                    WaterTypeVisible = true;
                    break;

                case CategoryType.Insect:
                    CanFlyVisible = true;
                    break;

                case CategoryType.Mammal:
                    NumberOfLegsVisible = true;
                    break;

                case CategoryType.Reptile:
                    HasScalesVisible = true;
                    break;

                default:
                    break;
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

        #endregion

        #region Animal properties mirroring Model Layer

        //Animal properties mirroring Model Layer

        // Animal
        public Animal Animal { get; set; }
        public string? Name { get; set; }
        public string Id { get; set; }
        public int Age { get; set; }
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
        public WaterHabitatType WaterType { get; set; }
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
        public HuntingTechniqueType HuntingTechnique { get; set; }
        public int MaxAgeInYears { get; set; }

        #endregion

        /// <summary>
        /// Initializes the mapping between categories and species.
        /// </summary>
        private Dictionary<CategoryType, List<string>> InitializeCategoryToSpeciesMap()
        {
            return new Dictionary<CategoryType, List<string>>
            {
                { CategoryType.Amphibian, new List<string> { Frog, Axolotl } },
                { CategoryType.Arachnid, new List<string> { Spider, Scorpion } },
                { CategoryType.Bird, new List<string> { Raven, Falcon } },
                { CategoryType.Fish, new List<string> { Salmon, Shark } },
                { CategoryType.Insect, new List<string> { Bee, Ladybug } },
                { CategoryType.Mammal, new List<string> { Cat, Elephant } },
                { CategoryType.Reptile, new List<string> { Snake, Tortoise } },
            };
        }

        /// <summary>
        /// Initializes the mapping between species and their creation functions.
        /// </summary>
        private Dictionary<string, Func<Animal>> InitializeSpeciesCreationMap()
        {
            return new Dictionary<string, Func<Animal>>
            {
                { Frog, () => new Frog(AnimalIdGenerator.GenerateId(Frog), Age, SelectedGender, Name, Landliving, Color) },
                { Axolotl, () => new Axolotl(AnimalIdGenerator.GenerateId(Axolotl), Age, SelectedGender, Name, Landliving, RegenerationRate) },
                { Spider, () => new Spider(AnimalIdGenerator.GenerateId(Spider), Age, SelectedGender, Name, Venomous, WebWeaving) },
                { Scorpion, () => new Scorpion(AnimalIdGenerator.GenerateId(Scorpion), Age, SelectedGender, Name, Venomous, Nocturnal) },
                { Raven, () => new Raven(AnimalIdGenerator.GenerateId(Raven), Age, SelectedGender, Name, Migratory, HasHatchling) },
                { Falcon, () => new Falcon(AnimalIdGenerator.GenerateId(Falcon), Age, SelectedGender, Name, Migratory, DivingSpeed) },
                { Salmon, () => new Salmon(AnimalIdGenerator.GenerateId(Salmon), Age, SelectedGender, Name, WaterType, HasBeenCaught) },
                { Shark, () => new Shark(AnimalIdGenerator.GenerateId(Shark), Age, SelectedGender, Name, WaterType, NumberOfGills) },
                { Bee, () => new Bee(AnimalIdGenerator.GenerateId(Bee), Age, SelectedGender, Name, CanFly, Solitary) },
                { Ladybug, () => new Ladybug(AnimalIdGenerator.GenerateId(Ladybug), Age, SelectedGender, Name, CanFly, NumberOfSpots) },
                { Cat, () => new Cat(AnimalIdGenerator.GenerateId(Cat), Age, SelectedGender, Name, NumberOfLegs, Breed) },
                { Elephant, () => new Elephant(AnimalIdGenerator.GenerateId(Elephant), Age, SelectedGender, Name, NumberOfLegs, TrunkLength) },
                { Snake, () => new Snake(AnimalIdGenerator.GenerateId(Snake), Age, SelectedGender, Name, HasScales, HuntingTechnique) },
                { Tortoise, () => new Tortoise(AnimalIdGenerator.GenerateId(Tortoise), Age, SelectedGender, Name, HasScales, MaxAgeInYears) },
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

        /// <summary>
        /// Creates an instance of the animal class corresponding to the currently selected species.
        /// </summary>
        public Animal CreateAnimalFromSelectedSpecies()
        {
            if (_selectedSpecies is not null && _speciesCreationMap.TryGetValue(_selectedSpecies, out Func<Animal>? createAnimal))
            {
                if (createAnimal is not null)
                {
                    return createAnimal();
                }
            }
            return null;
        }

        /// <summary>
        /// Creates a new animal and adds it to the collection.
        /// </summary>
        private void CreateAnimal()
        {
            Animal newAnimal = CreateAnimalFromSelectedSpecies();
            if (newAnimal is not null)
            {
                Animals.Add(newAnimal);
                SelectedAnimal = newAnimal;
            }
        }

        /// <summary>
        /// Updates the available species based on the selected category.
        /// </summary>
        private void UpdateAvailableSpeciesForCategory()
        {
            if (_categoryToSpeciesMap.TryGetValue(key: _selectedCategory, value: out List<string>? speciesList))
            {
                AvailableSpecies.Clear();

                if (speciesList is not null)
                {
                    foreach (string species in speciesList)
                    {
                        AvailableSpecies.Add(species);
                    }
                }
            }
        }

        /// <summary>
        /// Finds the category for a given species.
        /// </summary>
        private CategoryType FindCategoryForSpecies(string species)
        {
            foreach (var entry in _categoryToSpeciesMap)
            {
                if (entry.Value.Contains(species))
                {
                    return entry.Key;
                }
            }
            throw new Exception("Species does not belong to a category");
        }

        /// <summary>
        /// Generates a string representation of the properties of the currently selected animal with the help of ObjectInspectorHelper class.
        /// </summary>
        private string DisplaySelectedAnimalInfo()
        {
            if (SelectedAnimal is not null)
            {
                return ObjectInspectionHelper.GetPropertiesToString(SelectedAnimal);
            }
            else
            {
                Debug.WriteLine("_selectedAnimal is null");
                return string.Empty;
            }
        }

        /// <summary>
        /// Represents the state of the "List All Animals" checkbox in the UI.
        /// When this property is set, it triggers the update of the Animals collection.
        /// </summary>
        [DependsOn(nameof(IsCategoryEnabled))]
        public bool IsListAllAnimalsChecked { get; set; }

        /// <summary>
        /// Updates the available species when the 'List All Animals' checkbox is checked or unchecked.
        /// </summary>
        public void OnListAllAnimalsCheckedChanged()
        {
            if (IsListAllAnimalsChecked)
            {
                AvailableSpecies.Clear();
                foreach (var speciesList in _categoryToSpeciesMap.Values)
                {
                    foreach (string species in speciesList)
                    {
                        AvailableSpecies.Add(species);
                    }
                }
            }
            else
            {
                UpdateAvailableSpeciesForCategory();
            }
        }

        /// <summary>
        /// Determines whether the category selection is enabled in the UI.
        /// </summary>
        public bool IsCategoryEnabled
        {
            get
            {
                return !IsListAllAnimalsChecked;
            }
        }

        /// <summary>
        /// Converts a file by filepath to a BitmapImage.
        /// </summary>
        public BitmapImage FileToBitmapImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Sets property AnimalImage to the image chosen by the user.
        /// </summary>
        private void DisplayImage() => AnimalImage = FileToBitmapImage(OpenFileDialog(ImageFilters));

        /// <summary>
        /// Opens a file dialog for the user to select a file based on the filters.
        /// </summary>
        /// <param name="filter">File filters as strings</param>
        /// <returns>Filepath to the chosen file</returns>
        public string OpenFileDialog(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = filter };
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }
    }
}