using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Food.AddFood
{
    public sealed record AddFoodCommand(string FoodName) : ICommand<IEnumerable<FoodModel>>
    {
    }
}
