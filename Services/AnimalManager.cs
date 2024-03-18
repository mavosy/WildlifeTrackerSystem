using WTS.Services.Interfaces;
using WTS.Utilities;
using WTS.ViewModels;
using WTS.ViewModels.Comparers;

namespace WTS.Services
{
    /// <summary>
    /// Manages a collection of AnimalListItemViewModels, providing methods to manipulate and retrieve animal data.
    /// </summary>
    public class AnimalManager : IAnimalManager
    {
        private List<AnimalListItemViewModel> _animalList;
        private string _currentSortingProperty;
        private bool _ascendingSort;

        public AnimalManager()
        {
            _animalList = new List<AnimalListItemViewModel>();
        }

        /// <summary>
        /// Returns the count of animals in the list.
        /// </summary>
        public int CountAnimals()
        {
            return _animalList.Count;
        }

        /// <summary>
        /// Adds an AnimalListItemViewModel to the animal list.
        /// </summary>
        /// <param name="animalItem">The AnimalListItemViewModel to add.</param>
        /// <returns>True if the animal is successfully added, otherwise false.</returns>
        public bool Add(AnimalListItemViewModel animalItem)
        {
            if (animalItem == null) { return false; }
            _animalList.Add(animalItem);
            return true;
        }

        /// <summary>
        /// Retrieves an AnimalListItemViewModel at a specified index.
        /// </summary>
        /// <param name="index">The index to retrieve the animal from.</param>
        /// <returns>The AnimalListItemViewModel at the specified index.</returns>
        public AnimalListItemViewModel GetAnimalAt(int index)
        {
            if (!IsIndexInRange(index))
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
            return _animalList[index];
        }

        /// <summary>
        /// Sorts the list of animals a specified property. First default for all properties
        /// except Age is ascending sort.
        /// </summary>
        /// <param name="parameter">The property to sort the animals by as an object set from UI.</param>
        public void SortAnimalList(object parameter)
        {
            if (parameter is string sortingProperty)
            {
                if (_currentSortingProperty != sortingProperty)
                {
                    _ascendingSort = sortingProperty != "Age";
                    _currentSortingProperty = sortingProperty;
                }
                else
                {
                    _ascendingSort = !_ascendingSort;
                }

                var comparer = CreateComparerBasedOn(sortingProperty);

                if (comparer != null)
                {
                    _animalList.Sort(comparer);
                    if (!_ascendingSort)
                    {
                        _animalList.Reverse();
                    }
                }
                else
                {
                    throw new NullReferenceException("The comparer is null");
                }
            }
            else
            {
                throw new ArgumentException("The parameter provided to the sorting method is not a valid string onject");
            }

        }

        /// <summary>
        /// Creates an IComparer instance for sorting animals as AnimalListItemViewModels based on the specified property.
        /// </summary>
        /// <param name="sortingProperty">The property to sort by.</param>
        /// <returns>An IComparer for the specified property.</returns>
        private IComparer<AnimalListItemViewModel>? CreateComparerBasedOn(string sortingProperty)
        {
            return sortingProperty switch
            {
                "Id" => new AnimalIdComparer(),
                "Name" => new AnimalNameComparer(),
                "Age" => new AnimalAgeComparer(),
                "Category" => new AnimalCategoryComparer(),
                "Species" => new AnimalSpeciesComparer(),
                "Gender" => new AnimalGenderComparer(),
                _ => null
            };
        }

        /// <summary>
        /// Checks if a given index is within the range of the animal list.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if the index is within range, false otherwise.</returns>
        public bool IsIndexInRange(int index) => index >= 0 && index < _animalList.Count;
    }
}