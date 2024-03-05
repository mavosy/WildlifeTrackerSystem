using PropertyChanged;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
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
using WTS.Services.Interfaces;
using WTS.Utilities;
// TODO Continue with validation errors. When a minus is used in age field it gives arror, 
// but when removed (age is nullable) its still showing error. Also restrict all signs to not allow injection etc.
// also check if tooltip works, and test validation thoroughly.
// Also throw away trash code in VM. ObserverInsepctor and a lot more.
namespace WTS.ViewModels
{
    /// <summary>
    /// Represents the main ViewModel for the application, handling interactions between the View and the Model as part of the MVVM pattern.
    /// </summary>
    class WTSViewModel : BaseViewModel, INotifyDataErrorInfo
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
        private readonly IFileService _fileService;

        private readonly Dictionary<CategoryType, List<string>> _categoryToSpeciesMap;
        private readonly Dictionary<string, Func<Animal>> _speciesCreationMap;

        private readonly Dictionary<CategoryType, Action> _categoryVisibilityMap;
        private readonly Dictionary<string, Action> _speciesVisibilityMap;

        private readonly Dictionary<string, List<string>> _validationErrorsMap;

        private CategoryType _selectedCategory;
        private string _selectedSpecies;
        private Animal selectedAnimal;

        /// <summary>
        /// Constructor of WTSViewModel, initializes a new instance of the WTSViewModel class.
        /// </summary>
        public WTSViewModel(IFileService fileService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));

            _categoryToSpeciesMap = InitializeCategoryToSpeciesMap();
            _speciesCreationMap = InitializeSpeciesCreationMap();

            _categoryVisibilityMap = InitializeCategoryVisibilityMap();
            _speciesVisibilityMap = InitializeSpeciesVisibilityMap();

            _validationErrorsMap = InitializeValidationErrorMap();

            AvailableSpecies = new ObservableCollection<string>();
            Animals = new ObservableCollection<Animal>();
            AnimalInformation = new ObservableCollection<KeyValuePair<string, string>>();
            InitializeCommands();
        }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

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
                SelectedCategory = FindCategoryForSpecies(value);
            }
        }

        /// <summary>
        /// SelectedAnimal property represents the currently selected animal in the UI.
        /// When this property is set, it triggers the update of the SelectedAnimalInformation property through the fody attribute.
        /// </summary>
        public Animal SelectedAnimal { get => selectedAnimal; set { selectedAnimal = value; DisplaySelectedAnimalInfo(); } }

        /// <summary>
        /// Rread-only property that provides a detailed description of the currently selected animal.
        /// It depends on the SelectedAnimal property and calls the DisplaySelectedAnimalInfo method to generate the description.
        /// </summary>
        //[DependsOn(nameof(SelectedAnimal))]
        //public string SelectedAnimalInformation => DisplaySelectedAnimalInfo();

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

        [DependsOn(nameof(SelectedAnimal))]
        public ObservableCollection<KeyValuePair<string, string>> AnimalInformation { get; private set; }

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

        public bool HasErrors => _validationErrorsMap.Any(kv => kv.Value != null && kv.Value.Count > 0);

        /// <summary>
        /// Represents the state of the "List All Animals" checkbox in the UI.
        /// When this property is set, it triggers the update of the Animals collection.
        /// </summary>
        [DependsOn(nameof(IsCategoryEnabled))]
        public bool IsListAllAnimalsChecked { get; set; }

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

        //Booleans for UI visibility collapse
        public bool LandlivingVisible { get; set; }
        public bool ColorVisible { get; set; }
        public bool RegenerationRateVisible { get; set; }
        public bool VenomousVisible { get; set; }
        public bool WebWeavingVisible { get; set; }
        public bool NocturnalVisible { get; set; }
        public bool MigratoryVisible { get; set; }
        public bool HasHatchlingVisible { get; set; }
        public bool DivingSpeedVisible { get; set; }
        public bool WaterTypeVisible { get; set; }
        public bool HasBeenCaughtVisible { get; set; }
        public bool NumberOfGillsVisible { get; set; }
        public bool CanFlyVisible { get; set; }
        public bool SolitaryVisible { get; set; }
        public bool NumberOfSpotsVisible { get; set; }
        public bool NumberOfLegsVisible { get; set; }
        public bool BreedVisible { get; set; }
        public bool TrunkLengthVisible { get; set; }
        public bool HasScalesVisible { get; set; }
        public bool HuntingTechniqueVisible { get; set; }
        public bool MaxAgeInYearsVisible { get; set; }

        #region Animal properties mirroring Model Layer

        //Animal properties, most are bound to UI and transfered to constructor at moment of Animal object creation

        // Animal
        public Animal Animal { get; set; }
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
        /// Creates a new animal and adds it to the collection.
        /// </summary>
        private void CreateAnimal()
        {
            if (!HasErrors)
            {
                Animal newAnimal = CreateAnimalFromSelectedSpecies();
                if (newAnimal is not null)
                {
                    Animals.Add(newAnimal);
                    SelectedAnimal = newAnimal;
                }
            }
            else
            {
                MessageBox.Show("There are some errors in your input. Please correct them according to the instructions before proceeding.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Creates an instance of the animal class corresponding to the currently selected species.
        /// </summary>
        private Animal CreateAnimalFromSelectedSpecies()
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
        /// Updates the available species based on the selected category.
        /// </summary>
        private void UpdateAvailableSpeciesForCategory()
        {
            if (_categoryToSpeciesMap.TryGetValue(_selectedCategory, out List<string>? speciesList))
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
        private void DisplaySelectedAnimalInfo()
        {
            if (SelectedAnimal is not null)
            {
                AnimalInformation.Clear();
                foreach (KeyValuePair<string, ValueWrapper> keyValuePair in SelectedAnimal.GetPropertiesAsKeyValuePairs())
                {
                    string keyWithColon = keyValuePair.Key + ':';
                    KeyValuePair<string, string> keyValueStringPair = new KeyValuePair<string, string>(keyWithColon, keyValuePair.Value.ToString());
                    AnimalInformation.Add(keyValueStringPair);
                }
            }
        }

        ///// <summary>
        ///// Generates a string representation of the properties of the currently selected animal with the help of ObjectInspectorHelper class.
        ///// </summary>
        //private string DisplaySelectedAnimalInfo()
        //{
        //    if (SelectedAnimal is not null)
        //    {
        //        return SelectedAnimal.ToString();
        //    }
        //    else
        //    {
        //        Debug.WriteLine("_selectedAnimal is null");
        //        return string.Empty;
        //    }
        //}

        ///// <summary>
        ///// Generates a string representation of the properties of the currently selected animal with the help of ObjectInspectorHelper class.
        ///// </summary>
        //private string DisplaySelectedAnimalInfo()
        //{
        //    if (SelectedAnimal is not null)
        //    {
        //        return ObjectInspector.GetPropertiesToString(SelectedAnimal);
        //    }
        //    else
        //    {
        //        Debug.WriteLine("_selectedAnimal is null");
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// Updates the available species when the 'List All Animals' checkbox is checked or unchecked.
        /// </summary>
        private void OnIsListAllAnimalsCheckedChanged()
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
        /// Sets property AnimalImage to the image chosen by the user.
        /// </summary>
        private void DisplayImage()
        {
            try
            {
                string? filePath = _fileService.OpenFileDialog(ImageFilters);
                AnimalImage = _fileService.FileToBitmapImage(filePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while setting image: {ex}");
            }
        }

        protected void ValidateProperty(object value, [CallerMemberName] string? propertyName = null)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this) { MemberName = propertyName };
            Validator.TryValidateProperty(value, context, results);

            if (results.Count != 0)
            {
                _validationErrorsMap[propertyName] = results.ConvertAll(r => r.ErrorMessage);
            }
            else
            {
                _validationErrorsMap.Remove(propertyName);
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrorsMap.TryGetValue(propertyName, out List<string>? value))
            {
                return null;
            }
            return value;
        }

        /// <summary>
        /// Initializes the commands used in the ViewModel.
        /// </summary>
        private void InitializeCommands()
        {
            CreateAnimalCommand = new RelayCommand(_ => CreateAnimal());
            ListAllAnimalsCommand = new RelayCommand(_ => OnIsListAllAnimalsCheckedChanged());
            AddAnimalImageCommand = new RelayCommand(_ => DisplayImage());
        }

        /// <summary>
        /// Updates the visibility of the input fields related to the currently selected category.
        /// </summary>
        private void UpdateCategoryInputVisibility()
        {
            ResetCategoryInputVisibility();

            if (_categoryVisibilityMap.TryGetValue(SelectedCategory, out Action setVisibility))
            {
                setVisibility.Invoke();
            }
        }

        /// <summary>
        /// Updates the visibility of the input fields related to the currently selected species.
        /// </summary>
        private void UpdateSpeciesInputVisibility()
        {
            ResetSpeciesInputVisibility();

            if (SelectedSpecies is not null && _speciesVisibilityMap.TryGetValue(SelectedSpecies, out Action? setVisibility))
            {
                setVisibility?.Invoke();
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

        /// <summary>
        /// Initializes the mapping between categories and species.
        /// </summary>
        private static Dictionary<CategoryType, List<string>> InitializeCategoryToSpeciesMap()
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
                { Frog, () => new Frog(AnimalIdGenerator.GenerateId(Frog), Name, Age, SelectedGender, Landliving, Color) },
                { Axolotl, () => new Axolotl(AnimalIdGenerator.GenerateId(Axolotl), Name, Age, SelectedGender, Landliving, RegenerationRate) },
                { Spider, () => new Spider(AnimalIdGenerator.GenerateId(Spider), Name, Age, SelectedGender, Venomous, WebWeaving) },
                { Scorpion, () => new Scorpion(AnimalIdGenerator.GenerateId(Scorpion), Name, Age, SelectedGender, Venomous, Nocturnal) },
                { Raven, () => new Raven(AnimalIdGenerator.GenerateId(Raven), Name, Age, SelectedGender, Migratory, HasHatchling) },
                { Falcon, () => new Falcon(AnimalIdGenerator.GenerateId(Falcon), Name, Age, SelectedGender, Migratory, DivingSpeed) },
                { Salmon, () => new Salmon(AnimalIdGenerator.GenerateId(Salmon), Name, Age, SelectedGender, WaterType, HasBeenCaught) },
                { Shark, () => new Shark(AnimalIdGenerator.GenerateId(Shark), Name, Age, SelectedGender, WaterType, NumberOfGills) },
                { Bee, () => new Bee(AnimalIdGenerator.GenerateId(Bee), Name, Age, SelectedGender, CanFly, Solitary) },
                { Ladybug, () => new Ladybug(AnimalIdGenerator.GenerateId(Ladybug), Name, Age, SelectedGender, CanFly, NumberOfSpots) },
                { Cat, () => new Cat(AnimalIdGenerator.GenerateId(Cat), Name, Age, SelectedGender, NumberOfLegs, Breed) },
                { Elephant, () => new Elephant(AnimalIdGenerator.GenerateId(Elephant), Name, Age, SelectedGender, NumberOfLegs, TrunkLength) },
                { Snake, () => new Snake(AnimalIdGenerator.GenerateId(Snake), Name, Age, SelectedGender, HasScales, HuntingTechnique) },
                { Tortoise, () => new Tortoise(AnimalIdGenerator.GenerateId(Tortoise), Name, Age, SelectedGender, HasScales, MaxAgeInYears) },
            };
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
                [Frog] = () => ColorVisible = true,
                [Axolotl] = () => RegenerationRateVisible = true,
                [Spider] = () => WebWeavingVisible = true,
                [Scorpion] = () => NocturnalVisible = true,
                [Raven] = () => HasHatchlingVisible = true,
                [Falcon] = () => DivingSpeedVisible = true,
                [Salmon] = () => HasBeenCaughtVisible = true,
                [Shark] = () => NumberOfGillsVisible = true,
                [Bee] = () => SolitaryVisible = true,
                [Ladybug] = () => NumberOfSpotsVisible = true,
                [Cat] = () => BreedVisible = true,
                [Elephant] = () => TrunkLengthVisible = true,
                [Snake] = () => HuntingTechniqueVisible = true,
                [Tortoise] = () => MaxAgeInYearsVisible = true,
            };
        }

        private Dictionary<string, List<string>> InitializeValidationErrorMap()
        {
            return new Dictionary<string, List<string>>();
        }
    }
}