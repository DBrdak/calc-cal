using CalcCal.Domain.Shared;
using CalcCal.Domain.Users;

namespace CalcCal.Application.Models;

public record UserSimpleModel : EntityBusinessModel
{
    public string UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Username { get; init; }
    public string CountryCode { get; init; }
    public string PhoneNumber { get; init; }
    public bool IsPhoneNumberVerified { get; init; }

    protected UserSimpleModel(
        string userId,
        string firstName,
        string lastName,
        string username,
        string countryCode,
        string phoneNumber,
        bool isPhoneNumberVerified,
        IEnumerable<IDomainEvent> domainEvents) : base(domainEvents)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        PhoneNumber = phoneNumber;
        IsPhoneNumberVerified = isPhoneNumberVerified;
        CountryCode = countryCode;
    }

    internal static UserSimpleModel FromDomainObject(User domainObject)
    {
        return new UserSimpleModel(
            domainObject.Id.Id,
            domainObject.FirstName.Value,
            domainObject.LastName.Value,
            domainObject.Username.Value,
            domainObject.PhoneNumber.CountryCode,
            domainObject.PhoneNumber.Value,
            domainObject.IsPhoneNumberVerified,
            domainObject.GetDomainEvents());
    }
}