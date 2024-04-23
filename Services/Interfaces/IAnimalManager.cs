using WTS.ViewModels;

namespace WTS.Services.Interfaces
{
    public interface IAnimalManager : IListManager<AnimalListItemViewModel>
    {
        void SortAnimalList(object parameter);
        void ToggleSortingOrderFor(string sortingProperty);
    }
}