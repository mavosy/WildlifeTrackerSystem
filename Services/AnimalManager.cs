using WTS.Services.Interfaces;
using WTS.ViewModels;
using WTS.ViewModels.Comparers;

namespace WTS.Services
{
    /// <summary>
    /// Manages a collection of AnimalListItemViewModels, providing methods to manipulate and retrieve animal data.
    /// </summary>
    public class AnimalManager : ListManager<AnimalListItemViewModel>, IAnimalManager
    {
        private string _currentSortingProperty;
        private bool _ascendingSort;

        /// <summary>
        /// Sorts the list of animals a specified property. First default for all properties
        /// except Age is ascending sort.
        /// </summary>
        /// <param name="parameter">The property to sort the animals by as an object set from UI.</param>
        public void SortAnimalList(object parameter)
        {
            if (parameter is not string sortingProperty)
            {
                throw new ArgumentException("The parameter provided to the sorting method is not a valid string onject", nameof(parameter));
            }

            var comparer = CreateComparerBasedOn(sortingProperty);
            if (comparer is null)
            {
                throw new InvalidOperationException($"No comparer found for the sorting property '{sortingProperty}'.");
            }

            SortItems(comparer);

            ToggleSortingOrderFor(sortingProperty);

            if (!_ascendingSort)
            {
                ReverseItems();
            }
        }

        public void ToggleSortingOrderFor(string sortingProperty)
        {
            if (_currentSortingProperty == sortingProperty)
            {
                _ascendingSort = !_ascendingSort;
            }
            else
            {
                _currentSortingProperty = sortingProperty;
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
    }
}