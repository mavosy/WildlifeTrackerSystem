using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.ViewModels
{
    public class AnimalListItemViewModel : BaseViewModel
    {
        public Animal Animal { get; private set; }
        public string Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }

        public AnimalListItemViewModel(Animal animal)
        {
            Animal = animal;
            Id = animal.Id;
            Name = animal.Name;
            Age = animal.Age;
            Category = animal.Category.ToString();
            Gender = animal.Gender.ToString();
        }

        public IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            return Animal.GetPropertiesAsKeyValuePairs();
        }
    }
}