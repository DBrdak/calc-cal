using CalcCal.Domain.Shared;
using Responses.DB;
using System.Text.RegularExpressions;

namespace CalcCal.Domain.Users;

public sealed record FirstName : ValueObject<string>
{
    private const string valuePattern = @"^[a-zA-Z]{1,50}$";

    private FirstName(string value) : base(value)
    { }

    public static Result<FirstName> Create(string value)
    {
        if (!Regex.IsMatch(value, valuePattern))
        {
            return Result.Failure<FirstName>(UserErrors.InvalidFirstName);
        }

        return new FirstName(value.CapitalizeFirstLetter());
    }
}