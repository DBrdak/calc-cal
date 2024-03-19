using System.Security.Claims;
using CalcCal.Domain.Users;

namespace CalcCal.Infrastructure.Authentication.Models;

public sealed record UserRepresentationModel
{
    public static readonly string CreatedTimestampClaimName = nameof(CreatedTimestamp);
    public long CreatedTimestamp { get; init; }

    public static readonly string CountryCodeClaimName = nameof(CountryCode);
    public string CountryCode { get; init; }

    public static readonly string PhoneNumberClaimName = nameof(PhoneNumber);
    public string PhoneNumber { get; init; }

    public static readonly string PhoneNumberVerifiedClaimName = nameof(PhoneNumberVerified);
    public bool PhoneNumberVerified { get; init; }

    public static readonly string IdClaimName = nameof(Id);
    public string Id { get; init; }

    public static readonly string UsernameClaimName = nameof(Username);
    public string Username { get; init; }

    public static readonly string FirstNameClaimName = nameof(FirstName);
    public string FirstName { get; init; }

    public static readonly string LastNameClaimName = nameof(LastName);
    public string LastName { get; init; }

    public UserRepresentationModel(long createdTimestamp, string countryCode, string phoneNumber, bool phoneNumberVerified, string id, string username, string firstName, string lastName)
    {
        CreatedTimestamp = createdTimestamp;
        CountryCode = countryCode;
        PhoneNumber = phoneNumber;
        PhoneNumberVerified = phoneNumberVerified;
        Id = id;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }

    internal static UserRepresentationModel FromUser(User user) =>
        new(
            DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            user.PhoneNumber.CountryCode,
            user.PhoneNumber.Value,
            user.IsPhoneNumberVerified,
            user.Id.Id.ToString(),
            user.Username.Value,
            user.FirstName.Value,
            user.LastName.Value);

    internal Claim[] ToClaims()
    {
        return new[]
        {
            new Claim(CreatedTimestampClaimName, CreatedTimestamp.ToString()),
            new Claim(CountryCodeClaimName, CountryCode),
            new Claim(PhoneNumberClaimName, PhoneNumber),
            new Claim(PhoneNumberVerifiedClaimName, PhoneNumberVerified.ToString()),
            new Claim(IdClaimName, Id),
            new Claim(UsernameClaimName, Username),
            new Claim(FirstNameClaimName, FirstName),
            new Claim(LastNameClaimName, LastName)
        };
    }
}