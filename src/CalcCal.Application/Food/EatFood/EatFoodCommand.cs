using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Food.EatFood
{
    public sealed record EatFoodCommand(string FoodId, decimal FoodQuantity) : ICommand<FoodModel>
    {
    }
}
