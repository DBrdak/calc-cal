using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Users;

public sealed record VerificationCode : ValueObject
{
    public string Value { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime ValidTo { get; init; }
    private const int validForMinutes = 2;
    private const string valuePattern = @"^\d{6}$";

    private VerificationCode(string value)
    {
        Value = value;
        CreatedAt = DateTime.UtcNow;
        ValidTo = CreatedAt.AddMinutes(validForMinutes);
    }

    public static Result<VerificationCode> Create(string value)
    {
        if (!Regex.IsMatch(value, valuePattern))
        {
            return Result.Failure<VerificationCode>(UserErrors.InvalidVerificationCode);
        }

        return new VerificationCode(value);
    }

    public bool IsValid() => DateTime.UtcNow < ValidTo;

    public bool Verify(string code) => Value == code;
}