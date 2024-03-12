using WTS.Utilities;
using WTS.ViewModels;

namespace WTS.Services.Interfaces
{
    public interface IAnimalManager
    {
        int CountAnimals();
        bool Add(AnimalListItemViewModel animalItem);
        AnimalListItemViewModel GetAnimalAt(int index);
        IEnumerable<AnimalListItemViewModel> GetAllAnimals();
        IEnumerable<KeyValuePair<string, ValueWrapper>> GetAnimalProperties(int index);
        IEnumerable<IEnumerable<KeyValuePair<string, ValueWrapper>>> GetAllAnimalProperties();
        IEnumerable<AnimalListItemViewModel> SortAnimalList(object parameter);
        bool IsIndexInRange(int index);
    }
}