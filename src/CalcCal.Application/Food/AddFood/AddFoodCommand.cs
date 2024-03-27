using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;

namespace CalcCal.Application.Food.AddFood
{
    public sealed record AddFoodCommand(string FoodName) : ICommand<IEnumerable<FoodModel>>
    {
    }
}
