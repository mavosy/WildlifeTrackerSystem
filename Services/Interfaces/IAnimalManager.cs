using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.Services.Interfaces
{
    public interface IAnimalManager
    {
        bool Add(Animal animal);
        int CountAnimals();
        Animal GetAnimalAt(int index);
        IEnumerable<Animal> GetAllAnimals();
        IEnumerable<IEnumerable<KeyValuePair<string, ValueWrapper>>> GetAllAnimalProperties();
        IEnumerable<KeyValuePair<string, ValueWrapper>> GetAnimalProperties(int index);
    }
}