using System.Text.RegularExpressions;
using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Foods;

public sealed record FoodName : ValueObject<string>
{
    private const string valuePattern = @"^[a-zA-Z0-9.-]{1,50}$";

    private FoodName(string value) : base(value)
    {
    }

    public static Result<FoodName> Create(string value)
    {
        if (!Regex.IsMatch(value, valuePattern))
        {
            return Result.Failure<FoodName>(FoodErrors.InvalidName);
        }

        return new FoodName(value.CapitalizeFirstLetter());
    }
}