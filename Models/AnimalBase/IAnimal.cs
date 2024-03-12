using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.AnimalBase
{
    internal interface IAnimal
    {
        string Id { get; set; }
        string? Name { get; set; }
        int? Age { get; set; }
        GenderType Gender { get; set; }
        CategoryType Category { get; set; }
        IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs();
        string GetAnimalSoundAsString();
        FoodSchedule GetFoodSchedule();
    }
}