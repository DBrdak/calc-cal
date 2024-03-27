using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;

namespace CalcCal.Application.Food.GetFood;

public sealed record GetFoodQuery(string? FoodName) : IQuery<IEnumerable<FoodModel>>
{
}