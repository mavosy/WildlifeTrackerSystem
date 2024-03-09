using WTS.Models.AnimalBase;
using WTS.Services.Interfaces;
using WTS.Utilities;

namespace WTS.Services
{
    public class AnimalManager : IAnimalManager
    {
        private List<Animal> _animalList;

        public AnimalManager()
        {
            _animalList = new List<Animal>();
        }

        public bool Add(Animal animal)
        {
            if (animal == null) { return false; }
            _animalList.Add(animal);
            return true;
        }

        public int CountAnimals()
        {
            return _animalList.Count;
        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            return _animalList;
        }

        public Animal GetAnimalAt(int index)
        {
            if (!IsIndexInRange(index))
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
            return _animalList[index];
        }

        public bool IsIndexInRange(int index) => index >= 0 && index < _animalList.Count;

        public IEnumerable<IEnumerable<KeyValuePair<string, ValueWrapper>>> GetAllAnimalProperties()
        {
            return _animalList.Select(animal => animal.GetPropertiesAsKeyValuePairs());
        }

        public IEnumerable<KeyValuePair<string, ValueWrapper>> GetAnimalProperties(int index)
        {
            if (!IsIndexInRange(index))
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
            return _animalList[index].GetPropertiesAsKeyValuePairs();
        }
    }
}