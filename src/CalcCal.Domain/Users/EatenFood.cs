using CalcCal.Domain.Foods;
using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Users;

public sealed record EatenFood : ValueObject
{
    public Food Food { get; init; }
    public Quantity Quantity { get; init; }
    public Calories CaloriesEaten { get; init; }
    public DateTime EatenDateTime { get; init; }

    private EatenFood(Food food, Quantity quantity, Calories caloriesEaten, DateTime eatenDateTime)
    {
        Food = food;
        Quantity = quantity;
        CaloriesEaten = caloriesEaten;
        EatenDateTime = eatenDateTime;
    }

    public static Result<EatenFood> Create(Food food, decimal quantity)
    {
        var quantityResult = Quantity.Create(quantity);

        if (quantityResult.IsFailure)
        {
            return Result.Failure<EatenFood>(quantityResult.Error);
        }

        var caloriesEatenResult = Calories.Create(CalculateCalories(food, quantityResult.Value));

        if (caloriesEatenResult.IsFailure)
        {
            return Result.Failure<EatenFood>(caloriesEatenResult.Error);
        }

        food.Eat();

        return new EatenFood(
            food, 
            quantityResult.Value, 
            caloriesEatenResult.Value, 
            DateTime.UtcNow);
    }

    private static decimal CalculateCalories(Food food, Quantity quantity) => food.Caloriers.Value * (quantity.Value / food.Weight.Value);
}