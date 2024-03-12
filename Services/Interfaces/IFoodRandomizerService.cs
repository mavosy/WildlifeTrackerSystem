using WTS.Enums;

namespace WTS.Services.Interfaces
{
    public interface IFoodRandomizerService
    {
        IEnumerable<string> GetRandomFoodItems(EaterType eaterType, int itemsPerMeal);
    }
}