using CalcCal.Domain.Shared;
using Responses.DB;
using System.Text.RegularExpressions;

namespace CalcCal.Domain.Users;

public sealed record LastName : ValueObject<string>
{
    private const string valuePattern = @"^[a-zA-Z]{1,50}$";


    private LastName(string value) : base(value)
    { }

    public static Result<LastName> Create(string value)
    {
        if (!Regex.IsMatch(value, valuePattern))
        {
            return Result.Failure<LastName>(UserErrors.InvalidLastName);
        }

        return new LastName(value.CapitalizeFirstLetter());
    }
}