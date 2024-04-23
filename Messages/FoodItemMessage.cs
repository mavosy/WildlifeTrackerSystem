using CommunityToolkit.Mvvm.Messaging.Messages;
using WTS.Models;

namespace WTS.Messages
{
    public class FoodItemMessage : ValueChangedMessage<FoodItem>
    {
        public FoodItemMessage(FoodItem foodItem) : base(foodItem)
        {
        }
    }
}