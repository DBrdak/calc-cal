using CalcCal.Domain.Foods;
using CalcCal.Domain.Shared;
using Responses.DB;
using System.Text.RegularExpressions;

namespace CalcCal.Domain.Users;

public record PhoneNumber : ValueObject<string>
{
    private const string valuePattern = @"^[\+][0-9]{1,3}[0-9]{8,11}$";

    private PhoneNumber(string value) : base(value)
    {
    }

    public static Result<PhoneNumber> Create(string value)
    {
        if (!Regex.IsMatch(value, valuePattern))
        {
            return Result.Failure<PhoneNumber>(UserErrors.InvalidPhoneNumber);
        }

        return new PhoneNumber(value);
    }
}