using CalcCal.Domain.Foods;
using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Users;

public sealed record EatenFood : ValueObject
{
    public Food Food { get; init; }
    public Quantity Quantity { get; init; }

    private EatenFood(Food food, Quantity quantity)
    {
        Food = food;
        Quantity = quantity;
    }

    public static Result<EatenFood> Create(Food food, decimal quantity)
    {
        var quantityResult = Quantity.Create(quantity);

        if (quantityResult.IsFailure)
        {
            return Result.Failure<EatenFood>(quantityResult.Error);
        }

        food.Eat();

        return new EatenFood(food, quantityResult.Value);
    }
}