using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Food.GetFood;

public sealed record GetFoodQuery(string? FoodName) : IQuery<IEnumerable<FoodModel>>
{
}