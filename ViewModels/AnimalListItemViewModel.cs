using WTS.Models.AnimalBase;
using WTS.Utilities;

namespace WTS.ViewModels
{
    /// <summary>
    /// View model listing the created animals in a ListView and their respective animal information and food schedule in TextBlocks
    /// </summary>
    public class AnimalListItemViewModel : BaseViewModel
    {
        public Animal Animal { get; private set; }
        public string Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public string Species { get; set; }

        public AnimalListItemViewModel(Animal animal)
        {
            Animal = animal;
            Id = animal.Id;
            Name = animal.Name;
            Age = animal.Age;
            Category = animal.Category.ToString();
            Gender = animal.Gender.ToString();
            Species = animal.GetType().Name;
        }

        public IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            yield return new KeyValuePair<string, ValueWrapper>("Species", ValueWrapper.Create(Species));

            foreach (var kvp in Animal.GetPropertiesAsKeyValuePairs())
            {
                yield return kvp;
            }
        }
    }
}