using System.Text.RegularExpressions;
using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Foods;

public sealed record FoodName : ValueObject<string>
{
    private const int valueMaxLength = 150;

    private FoodName(string value) : base(value)
    {
    }

    public static Result<FoodName> Create(string value)
    {
        if (value.Length > valueMaxLength)
        {
            return Result.Failure<FoodName>(FoodErrors.InvalidName);
        }

        return new FoodName(value);
    }
}