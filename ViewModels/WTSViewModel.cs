﻿using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using PropertyChanged;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WTS.Commands;
using WTS.Enums;
using WTS.Messages;
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
using WTS.Validators;
using WTS.Views;

namespace WTS.ViewModels
{
    /// <summary>
    /// Represents the main ViewModel for the application, handling interactions between the View and the Model as part of the MVVM pattern.
    /// </summary>
    public class WTSViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        // Constants
        private const string ImageFilters = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
        private const string SaveDataFilters = "Text File (*.txt)|*.txt|JSON File (*.json)|*.json|XML File (*.xml)|*.xml";
        private const string LoadDataFilters = "All Files (*.txt;*.json;*.xml)|*.txt;*.json;*.xml|Text Files (*.txt)|*.txt|JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml";



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

        // Dependency Injection fields
        private readonly IMessenger _messenger;
        private readonly IFileService _fileService;
        private readonly IDialogService _dialogService;
        private readonly IAnimalManager _animalManager;
        private readonly IFoodManager _foodManager;
        private readonly GeneralAnimalValidator _generalAnimalValidator;

        // Dictionary fields
        private readonly Dictionary<CategoryType, List<string>> _categoryToSpeciesMap;
        private readonly Dictionary<string, Func<Animal>> _speciesCreationMap;

        private readonly Dictionary<CategoryType, Action> _categoryVisibilityMap;
        private readonly Dictionary<string, Action> _speciesVisibilityMap;

        private readonly Dictionary<string, List<string>?> _validationErrorsMap;

        // Property fields
        private CategoryType _selectedCategory;
        private string _selectedSpecies;
        private AnimalListItemViewModel? _selectedAnimal;

        private string? name;
        private int? age;
        private string? color;
        private double? regenerationRate;
        private int? divingSpeed;
        private int? numberOfGills;
        private int? numberOfSpots;
        private int? numberOfLegs;
        private string? breed;
        private int? trunkLength;
        private int? maxAgeInYears;

        /// <summary>
        /// Constructor of WTSViewModel, initializes a new instance of the WTSViewModel class.
        /// </summary>
        public WTSViewModel(IMessenger messenger, IFileService fileService, IDialogService dialogService, IAnimalManager animalManager, IFoodManager foodManager, GeneralAnimalValidator generalAnimalValidator)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _animalManager = animalManager ?? throw new ArgumentNullException(nameof(animalManager));
            _foodManager = foodManager ?? throw new ArgumentNullException(nameof(foodManager));
            _generalAnimalValidator = generalAnimalValidator ?? throw new ArgumentNullException(nameof(generalAnimalValidator));

            _categoryToSpeciesMap = InitializeCategoryToSpeciesMap();
            _speciesCreationMap = InitializeSpeciesCreationMap();

            _categoryVisibilityMap = InitializeCategoryVisibilityMap();
            _speciesVisibilityMap = InitializeSpeciesVisibilityMap();

            _validationErrorsMap = InitializeValidationErrorMap();

            InitializeEnumDefaultValues();
            SubscribeToMessages();
            InitializeCommands();
            InitializeCollections();
            LoadAnimals();
        }

        /// <summary>
        /// Occurs when the validation errors for a property have changed.
        /// </summary>
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
                ResetCategoryInputValue();

                if (!IsListAllSpeciesChecked)
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
                ResetSpeciesInputValue();
                SelectedCategory = FindCategoryForSpecies(value);
            }
        }

        /// <summary>
        /// SelectedAnimal property represents the currently selected animal in the UI.
        /// When this property is set, it triggers the update of the SelectedAnimalInformation property through the fody attribute.
        /// </summary>
        public AnimalListItemViewModel? SelectedAnimal
        {
            get => _selectedAnimal;
            set
            {
                _selectedAnimal = value;
                DisplayAnimalInfo();
                DisplayAnimalFoodSchedule();
                DisplayAnimalEaterType();
            }
        }

        public FoodItem SelectedFoodItem { get; set; }

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
        public ObservableCollection<AnimalListItemViewModel> Animals { get; private set; }

        /// <summary>
        /// Contains information about the selected animal as a collection of key-value pairs, bound to UI Summary element.
        /// Updates whenever the SelectedAnimal property changes.
        /// </summary>
        [DependsOn(nameof(SelectedAnimal))]
        public ObservableCollection<KeyValuePair<string, string>> AnimalInformation { get; private set; }

        public ObservableCollection<FoodItem> FoodItems { get; private set; }

        /// <summary>
        /// Contains information about the animals food schedule.
        /// </summary>
        public string? FoodScheduleInfo { get; private set; }

        /// <summary>
        /// Contains information about what type of food the animal eats.
        /// </summary>
        public string? EaterTypeInfo { get; private set; }

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
                return !IsListAllSpeciesChecked;
            }
        }

        /// <summary>
        /// Indicates whether there are any validation errors in the ViewModel prioperties bound to the UI.
        /// </summary>
        public bool HasErrors => _validationErrorsMap.Any(kv => kv.Value?.Count > 0);

        /// <summary>
        /// Represents the state of the "List All Animals" checkbox in the UI.
        /// When this property is set, it triggers the update of the Animals collection.
        /// </summary>
        [DependsOn(nameof(IsCategoryEnabled))]
        public bool IsListAllSpeciesChecked { get; set; }

        /// <summary>
        /// Provides a command for creating a new animal.
        /// The command is bound to the "Create Animal" button in the UI, and it calls the CreateAnimal method when executed.
        /// </summary>
        public ICommand CreateAnimalCommand { get; private set; }

        /// <summary>
        /// Provides a command for creating a new animal.
        /// The command is bound to the "Create Animal" button in the UI, and it calls the CreateAnimal method when executed.
        /// </summary>
        public ICommand UpdateAnimalCommand { get; private set; }

        /// <summary>
        /// Provides a command for creating a new animal.
        /// The command is bound to the "Create Animal" button in the UI, and it calls the CreateAnimal method when executed.
        /// </summary>
        public ICommand DeleteAnimalCommand { get; private set; }

        /// <summary>
        /// Provides a command for listing all animals.
        /// The command is bound to the "List All Animals" button in the UI, and it updates the Animals collection when executed.
        /// </summary>
        public ICommand ListAllSpeciesCommand { get; private set; }

        /// <summary>
        /// Provides a command for adding an image to an animal.
        /// The command is bound to the "Add Image" button in the UI, and it calls the OpenFileDialog and FileToBitmapImage methods when executed.
        /// </summary>
        public ICommand AddAnimalImageCommand { get; private set; }

        /// <summary>
        /// Provides a command for sorting animals in the UI ListView of created animals.
        /// The command is bound to the headers of the ListView, provides a string object of the property to be sorted, 
        /// and calls the SortAnimals method when executed.
        /// </summary>
        public ICommand SortCommand { get; private set; }

        /// <summary>
        /// Provides a command for opening a windowed form to handle food items.
        /// </summary>
        public ICommand OpenFoodItemViewCommand { get; private set; }

        public ICommand PairAnimalWithFoodItemCommand { get; private set; }

        public ICommand SaveAnimalDataCommand { get; private set; }

        public ICommand LoadAnimalDataCommand { get; private set; }

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

        // Animal properties, most are bound to UI and transfered to constructor at moment of Animal object creation

        // Animal
        public Animal Animal { get; set; }
        public string Id { get; set; }
        public string? Name { get => name; set { name = value; ValidateProperty(); } }
        public int? Age { get => age; set { age = value; ValidateProperty(); } }

        public GenderType SelectedGender { get; set; }

        // Amphibian
        public bool Landliving { get; set; }
        public string? Color { get => color; set { color = value; ValidateProperty(); } }
        public double? RegenerationRate { get => regenerationRate; set { regenerationRate = value; ValidateProperty(); } }

        // Arachnid
        public bool Venomous { get; set; }
        public bool WebWeaving { get; set; }
        public bool Nocturnal { get; set; }

        // Bird
        public bool Migratory { get; set; }
        public bool HasHatchling { get; set; }
        public int? DivingSpeed { get => divingSpeed; set { divingSpeed = value; ValidateProperty(); } }

        // Fish
        public WaterHabitatType SelectedWaterType { get; set; }
        public bool HasBeenCaught { get; set; }
        public int? NumberOfGills { get => numberOfGills; set { numberOfGills = value; ValidateProperty(); } }

        // Insect
        public bool CanFly { get; set; }
        public bool Solitary { get; set; }
        public int? NumberOfSpots { get => numberOfSpots; set { numberOfSpots = value; ValidateProperty(); } }

        // Mammal
        public int? NumberOfLegs { get => numberOfLegs; set { numberOfLegs = value; ValidateProperty(); } }
        public string? Breed { get => breed; set { breed = value; ValidateProperty(); } }
        public int? TrunkLength { get => trunkLength; set { trunkLength = value; ValidateProperty(); } }

        // Reptile
        public bool HasScales { get; set; }
        public HuntingTechniqueType SelectedHuntingTechnique { get; set; }
        public int? MaxAgeInYears { get => maxAgeInYears; set { maxAgeInYears = value; ValidateProperty(); } }

        #endregion

        /// <summary>
        /// Creates a new animal, encapsulates it in a AnimalListItemViewModel object,
        /// adds it to a list in AnimalManager, and sets SelectedAnimal to the newly created animal.
        /// </summary>
        private void CreateAnimal()
        {
            if (!HasErrors)
            {
                Animal newAnimal = CreateAnimalFromSelectedSpecies();
                if (newAnimal is not null)
                {
                    AnimalListItemViewModel animalListItem = new AnimalListItemViewModel(newAnimal);
                    _animalManager.Add(animalListItem);
                    LoadAnimals();
                    SelectedAnimal = animalListItem;
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
            bool speciesIsSelected = SelectedSpecies is not null;
            if (speciesIsSelected && _speciesCreationMap.TryGetValue(SelectedSpecies, out Func<Animal>? createAnimal))
            {
                if (createAnimal is not null)
                {
                    return createAnimal();
                }
            }
            return null;
        }

        /// <summary>
        /// Creates a new animal, encapsulates it in a AnimalListItemViewModel object,
        /// and replaces the currently selected animal with the newly created one.
        /// </summary>
        private void UpdateAnimal()
        {
            if (!HasErrors && SelectedAnimal is not null)
            {
                Animal newAnimal = CreateAnimalFromSelectedSpecies();
                if (newAnimal is not null)
                {
                    AnimalListItemViewModel animalListItem = new AnimalListItemViewModel(newAnimal);

                    int selectedAnimalIndex = Animals.IndexOf(SelectedAnimal);
                    _animalManager.Replace(selectedAnimalIndex, animalListItem);
                    LoadAnimals();
                    SelectedAnimal = animalListItem;
                }
            }
            else
            {
                MessageBox.Show("There are some errors in your input. Please correct them according to the instructions before proceeding.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Deletes the selected animal from the Animals collection
        /// </summary>
        private void DeleteAnimal()
        {
            if (SelectedAnimal is not null)
            {
                int selectedAnimalIndex = Animals.IndexOf(SelectedAnimal);
                _animalManager.Delete(SelectedAnimal);
                LoadAnimals();

                SelectedAnimal = Animals.Count > 0
                    ? Animals[Math.Min(selectedAnimalIndex, Animals.Count - 1)]
                    : null;
            }
        }

        /// <summary>
        /// Provides created animal objects from the AnimalManager, encapsulated in a AnimalListItemViewModel object,
        /// to a collection bound to the UI.
        /// </summary>
        private void LoadAnimals()
        {
            var currentlySelectedAnimal = SelectedAnimal;
            if (Animals.Any())
            {
                Animals.Clear();
            }
            int animalListLength = _animalManager.Count;
            for (int i = 0; i < animalListLength; i++)
            {
                Animals.Add(_animalManager.GetItemAt(i));
            }
            SelectedAnimal = currentlySelectedAnimal;
        }

        private static void OpenFoodItemView()
        {
            var foodItemView = new FoodItemView();
            foodItemView.ShowDialog();
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
        /// Generates a string representation of the properties of the currently selected animal with an inherited KeyValuePair.
        /// </summary>
        private void DisplayAnimalInfo()
        {
            AnimalInformation.Clear();
            if (SelectedAnimal is not null)
            {
                foreach (KeyValuePair<string, ValueWrapper> keyValuePair in SelectedAnimal.GetPropertiesAsKeyValuePairs())
                {
                    string keyWithColon = keyValuePair.Key + ':';
                    string value = keyValuePair.Value.Value is null ? "Not entered"
                                                              : keyValuePair.Value.ToString();

                    KeyValuePair<string, string> keyValueStringPair = new KeyValuePair<string, string>(keyWithColon, value);
                    AnimalInformation.Add(keyValueStringPair);
                }
            }
        }

        /// <summary>
        /// Displays the currently selected animal's food schedule.
        /// </summary>
        private void DisplayAnimalFoodSchedule()
        {
            FoodScheduleInfo = null;
            if (SelectedAnimal is not null)
            {
                var foodItems = _foodManager.GetFoodItemsForAnimal(SelectedAnimal.Animal.DisplayName);
                FoodScheduleInfo = string.Join(", ", foodItems);
            }
        }

        /// <summary>
        /// Displays the currently selected animal's food consumption category.
        /// </summary>
        private void DisplayAnimalEaterType()
        {
            EaterTypeInfo = null;
            if (SelectedAnimal is not null)
            {
                EaterTypeInfo = $"Eater type: {SelectedAnimal.Animal.GetFoodSchedule().EaterType}";
            }
        }

        /// <summary>
        /// Shows all available species in the UI.
        /// </summary>
        private void OnIsListAllSpeciesCheckedChanged()
        {
            if (IsListAllSpeciesChecked)
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
                string filePath = _dialogService.OpenFileDialog(ImageFilters) ?? throw new Exception("No image selected");
                AnimalImage = _fileService.FileToBitmapImage(filePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while setting image: {ex}");
            }
        }

        /// <summary>
        /// Validates the caller property using the GeneralAnimalValidator.
        /// Adds any errors found to a dictionary.
        /// </summary>
        protected void ValidateProperty([CallerMemberName] string? propertyName = null)
        {
            var results = _generalAnimalValidator.Validate(this, options => options.IncludeProperties(propertyName));
            bool errorsChanged = false;

            if (results.IsValid && _validationErrorsMap.ContainsKey(propertyName))
            {
                _validationErrorsMap.Remove(propertyName);
                errorsChanged = true;
            }
            else
            {
                var newErrors = results.Errors
                                       .Where(e => e.PropertyName == propertyName)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

                if (!_validationErrorsMap.TryGetValue(propertyName, out var existingErrors) || !newErrors.SequenceEqual(existingErrors))
                {
                    _validationErrorsMap[propertyName] = newErrors;
                    errorsChanged = true;
                }
            }

            if (errorsChanged)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Retrieves the validation errors for a specified property.
        /// </summary>
        /// <returns>A collection of error messages for the specified property, or null if no errors are found.</returns>
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
            UpdateAnimalCommand = new RelayCommand(_ => UpdateAnimal());
            DeleteAnimalCommand = new RelayCommand(_ => DeleteAnimal());
            ListAllSpeciesCommand = new RelayCommand(_ => OnIsListAllSpeciesCheckedChanged());
            AddAnimalImageCommand = new RelayCommand(_ => DisplayImage());
            SortCommand = new RelayCommand(SortAnimals);
            OpenFoodItemViewCommand = new RelayCommand(_ => OpenFoodItemView());
            PairAnimalWithFoodItemCommand = new RelayCommand(PairAnimalWithFoodItem);
            SaveAnimalDataCommand = new RelayCommand(_ => SaveAnimalDataFromFile());
            LoadAnimalDataCommand = new RelayCommand(_ => LoadAnimalDataFromFile());
        }

        /// <summary>
        /// Initializes the ObservableCollections used in the ViewModel.
        /// </summary>
        private void InitializeCollections()
        {
            AvailableSpecies = new ObservableCollection<string>();
            Animals = new ObservableCollection<AnimalListItemViewModel>();
            AnimalInformation = new ObservableCollection<KeyValuePair<string, string>>();
            FoodItems = new ObservableCollection<FoodItem>();
        }

        /// <summary>
        /// Initializes default values to the enums used in the ViewModel.
        /// </summary>
        private void InitializeEnumDefaultValues()
        {
            SelectedGender = GenderType.Unknown;
            SelectedWaterType = WaterHabitatType.Unknown;
            SelectedHuntingTechnique = HuntingTechniqueType.Unknown;
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
        /// Sorts the animals in the ListView according to rules provided by their respective Comparer classes.
        /// </summary>
        /// <param name="parameter">An object containing information about which property name header is clicked in the ListView</param>
        private void SortAnimals(object parameter)
        {
            _animalManager.SortAnimalList(parameter);
            LoadAnimals();
        }

        private void SaveAnimalDataFromFile()
        {
            try
            {
                var filePath = _dialogService.SaveFileDialog(SaveDataFilters);
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (Path.GetExtension(filePath).Equals(".txt", StringComparison.CurrentCultureIgnoreCase))
                    {
                        _animalManager.SaveAsText(filePath);
                    }
                    else if (Path.GetExtension(filePath).Equals(".json", StringComparison.CurrentCultureIgnoreCase))
                    {
                        _animalManager.SaveAsJson(filePath);
                    }
                    else if (Path.GetExtension(filePath).Equals(".xml", StringComparison.CurrentCultureIgnoreCase))
                    {
                        SaveAnimalFoodItemsToXml(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to save data. Error: {ex.Message} {ex.InnerException}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveAnimalFoodItemsToXml(string fileName)
        {
            var animalNames = new List<string>();
            for (int i = 0; i < _animalManager.Count; i++)
            {
                var animal = _animalManager.GetItemAt(i);
                if (!string.IsNullOrEmpty(animal.Name))
                {
                    animalNames.Add(animal.Name);
                }
            }
            if (animalNames.Count != 0)
            {
                _foodManager.SaveAsXml(fileName, animalNames);
            }
            else
            {
                throw new Exception("No animals to save");
            }
        }

        private void LoadAnimalDataFromFile()
        {
            try
            {
                string? filePath = _dialogService.OpenFileDialog(LoadDataFilters);
                if (!string.IsNullOrEmpty(filePath))
                {
                    string? dataString = null;
                    if (Path.GetExtension(filePath).Equals(".txt", StringComparison.CurrentCultureIgnoreCase))
                    {
                        dataString = _animalManager.LoadText(filePath) ?? throw new NullReferenceException("Error loading data");
                    }
                    else if (Path.GetExtension(filePath).Equals(".json", StringComparison.CurrentCultureIgnoreCase))
                    {
                        dataString = _animalManager.LoadJson(filePath) ?? throw new NullReferenceException("Error loading data");
                    }
                    else if (Path.GetExtension(filePath).Equals(".xml", StringComparison.CurrentCultureIgnoreCase))
                    {
                        dataString = _foodManager.LoadFromXml(filePath) ?? throw new NullReferenceException("Error loading data");
                    }

                    ShowDataInPopup(dataString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load data. Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowDataInPopup(string? data)
        {
            if (string.IsNullOrEmpty(data)) return;
            _dialogService.DataDisplayDialog("Animal Data", data);
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
        /// Resets the input value of all category-related input fields to their default state.
        /// </summary>
        private void ResetCategoryInputValue()
        {
            Landliving = false;
            Venomous = false;
            Migratory = false;
            SelectedWaterType = WaterHabitatType.Unknown;
            CanFly = false;
            NumberOfLegs = null;
            HasScales = false;
        }

        /// <summary>
        /// Resets the input value of all species-related input fields to their default state.
        /// </summary>
        private void ResetSpeciesInputValue()
        {
            Color = null;
            RegenerationRate = null;
            WebWeaving = false;
            Nocturnal = false;
            HasHatchling = false;
            DivingSpeed = null;
            HasBeenCaught = false;
            NumberOfGills = null;
            Solitary = false;
            NumberOfSpots = null;
            Breed = null;
            TrunkLength = null;
            SelectedHuntingTechnique = HuntingTechniqueType.Unknown;
            MaxAgeInYears = null;
        }

        /// <summary>
        /// Initializes the mapping between categories and species.
        /// </summary>
        private static Dictionary<CategoryType, List<string>> InitializeCategoryToSpeciesMap()
        {
            return new Dictionary<CategoryType, List<string>>
            {
                [CategoryType.Amphibian] = new List<string> { Frog, Axolotl },
                [CategoryType.Arachnid] = new List<string> { Spider, Scorpion },
                [CategoryType.Bird] = new List<string> { Raven, Falcon },
                [CategoryType.Fish] = new List<string> { Salmon, Shark },
                [CategoryType.Insect] = new List<string> { Bee, Ladybug },
                [CategoryType.Mammal] = new List<string> { Cat, Elephant },
                [CategoryType.Reptile] = new List<string> { Snake, Tortoise },
            };
        }

        /// <summary>
        /// Initializes the mapping between species and their creation functions.
        /// </summary>
        private Dictionary<string, Func<Animal>> InitializeSpeciesCreationMap()
        {
            return new Dictionary<string, Func<Animal>>
            {
                [Frog] = () => new Frog(AnimalIdGenerator.GenerateId(Frog), Name, Age, SelectedGender, Landliving, Color),
                [Axolotl] = () => new Axolotl(AnimalIdGenerator.GenerateId(Axolotl), Name, Age, SelectedGender, Landliving, RegenerationRate),
                [Spider] = () => new Spider(AnimalIdGenerator.GenerateId(Spider), Name, Age, SelectedGender, Venomous, WebWeaving),
                [Scorpion] = () => new Scorpion(AnimalIdGenerator.GenerateId(Scorpion), Name, Age, SelectedGender, Venomous, Nocturnal),
                [Raven] = () => new Raven(AnimalIdGenerator.GenerateId(Raven), Name, Age, SelectedGender, Migratory, HasHatchling),
                [Falcon] = () => new Falcon(AnimalIdGenerator.GenerateId(Falcon), Name, Age, SelectedGender, Migratory, DivingSpeed),
                [Salmon] = () => new Salmon(AnimalIdGenerator.GenerateId(Salmon), Name, Age, SelectedGender, SelectedWaterType, HasBeenCaught),
                [Shark] = () => new Shark(AnimalIdGenerator.GenerateId(Shark), Name, Age, SelectedGender, SelectedWaterType, NumberOfGills),
                [Bee] = () => new Bee(AnimalIdGenerator.GenerateId(Bee), Name, Age, SelectedGender, CanFly, Solitary),
                [Ladybug] = () => new Ladybug(AnimalIdGenerator.GenerateId(Ladybug), Name, Age, SelectedGender, CanFly, NumberOfSpots),
                [Cat] = () => new Cat(AnimalIdGenerator.GenerateId(Cat), Name, Age, SelectedGender, NumberOfLegs, Breed),
                [Elephant] = () => new Elephant(AnimalIdGenerator.GenerateId(Elephant), Name, Age, SelectedGender, NumberOfLegs, TrunkLength),
                [Snake] = () => new Snake(AnimalIdGenerator.GenerateId(Snake), Name, Age, SelectedGender, HasScales, SelectedHuntingTechnique),
                [Tortoise] = () => new Tortoise(AnimalIdGenerator.GenerateId(Tortoise), Name, Age, SelectedGender, HasScales, MaxAgeInYears),
            };
        }

        /// <summary>
        /// Initializes a map linking each animal category to an action that sets the visibility of its specific properties in the UI.
        /// </summary>
        /// <returns>A dictionary mapping CategoryType to actions that update visibility properties.</returns>
        private Dictionary<CategoryType, Action> InitializeCategoryVisibilityMap()
        {
            return new Dictionary<CategoryType, Action>
            {
                [CategoryType.Amphibian] = () => LandlivingVisible = true,
                [CategoryType.Arachnid] = () => VenomousVisible = true,
                [CategoryType.Bird] = () => MigratoryVisible = true,
                [CategoryType.Fish] = () => WaterTypeVisible = true,
                [CategoryType.Insect] = () => CanFlyVisible = true,
                [CategoryType.Mammal] = () => NumberOfLegsVisible = true,
                [CategoryType.Reptile] = () => HasScalesVisible = true,
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

        /// <summary>
        /// Initializes an empty dictionary to store validation errors for properties.
        /// </summary>
        /// <returns>An empty dictionary for storing property validation errors.</returns>
        private static Dictionary<string, List<string>?> InitializeValidationErrorMap()
        {
            return new Dictionary<string, List<string>?>();
        }

        private void SubscribeToMessages()
        {
            _messenger.Register<FoodItemMessage>(this, (recipient, message) => FoodItems.Add(message.Value));
        }

        private void PairAnimalWithFoodItem(object obj)
        {
            if (SelectedFoodItem is not null && SelectedAnimal is not null)
            {
                _foodManager.PairAnimalWithFoodItem(SelectedAnimal.Animal.DisplayName, SelectedFoodItem.Name);
            }
            DisplayAnimalFoodSchedule();
        }
    }
}