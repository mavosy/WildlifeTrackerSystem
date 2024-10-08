using WTS.Services;
using WTS.Services.Interfaces;

namespace WTS.Models
{
    [Serializable]
    public class FoodItem
    {
        public string Name { get; set; }
        public ListManager<string> Ingredients { get; }

        public FoodItem()
        {
            Ingredients = new ListManager<string>();
        }

        public override string ToString()
        {
            return Ingredients.Count > 0
                ? string.Join(", ", Ingredients.ToStringList())
                : "List is empty";
        }
    }
}