using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;
using CalcCal.Domain.Users;

namespace CalcCal.Application.Food.EatFood
{
    public sealed record EatFoodCommand(string FoodId, decimal FoodQuantity) : ICommand<EatenFoodModel>
    {
    }
}
