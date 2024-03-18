using WTS.Utilities;
using WTS.ViewModels;

namespace WTS.Services.Interfaces
{
    public interface IAnimalManager
    {
        int CountAnimals();
        bool Add(AnimalListItemViewModel animalItem);
        AnimalListItemViewModel GetAnimalAt(int index);
        void SortAnimalList(object parameter);
        bool IsIndexInRange(int index);
    }
}