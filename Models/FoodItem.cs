using WTS.Services;

namespace WTS.Models
{
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
            if (Ingredients is not null && Ingredients.Count > 0)
            {
                string ingredientsString = null;
                Ingredients.ToStringList();
                foreach (string item in Ingredients.ToStringList())
                {
                    ingredientsString += item;
                }
                return ingredientsString;
            }
            else
            {
                return "List is empty";
            }
        }
    }
}