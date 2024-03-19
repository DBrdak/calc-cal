using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Foods;

public sealed record Calories : ValueObject<decimal>
{
    private const decimal maxValue = 25_000;
    private const decimal minValue = 0;

    private Calories(decimal value) : base(value)
    {
    }

    public static Result<Calories> Create(decimal value)
    {
        if (value is > minValue and < maxValue)
        {
            return Result.Failure<Calories>(FoodErrors.InvalidCalories);
        }

        return new Calories(value);
    }
}