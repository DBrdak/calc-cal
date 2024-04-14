using CalcCal.Domain.Shared;
using Responses.DB;
using System.Text.RegularExpressions;

namespace CalcCal.Domain.Users;

public record PhoneNumber : ValueObject
{
    public string Value { get; init; }
    public string CountryCode { get; init; }
    private const string valuePattern = @"^[0-9]{8,11}$";
    private const string countryCodePattern = @"^[+][0-9]{1,3}$";

    private PhoneNumber(string countryCode, string value)
    {
        CountryCode = countryCode;
        Value = value;
    }

    public static Result<PhoneNumber> Create(string countryCode, string value)
    {
        if (!Regex.IsMatch(value, valuePattern) || !Regex.IsMatch(countryCode, countryCodePattern))
        {
            return Result.Failure<PhoneNumber>(UserErrors.InvalidPhoneNumber);
        }

        return new PhoneNumber(countryCode, value);
    }

    public override string ToString() => $"{CountryCode}{Value}";
}