using System.Text.RegularExpressions;
using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Users;

public record Username : ValueObject<string>
{
    private const string valuePattern = @"^[a-zA-Z_.-]{1,50}$";

    private Username(string value) : base(value)
    {
    }

    public static Result<Username> Create(string value)
    {
        if (!Regex.IsMatch(value, valuePattern))
        {
            return Result.Failure<Username>(UserErrors.InvalidUsername);
        }

        return new Username(value);
    }

    public virtual bool Equals(Username? other)
    {
        return string.Equals(other?.Value, Value, StringComparison.CurrentCultureIgnoreCase);
    }
}