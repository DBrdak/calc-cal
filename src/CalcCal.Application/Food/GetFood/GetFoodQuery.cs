using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Food.GetFood;

public sealed record GetFoodQuery(string FoodName) : IQuery<List<Domain.Foods.Food>>
{
}