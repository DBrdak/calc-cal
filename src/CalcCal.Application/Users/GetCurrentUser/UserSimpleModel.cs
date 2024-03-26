using CalcCal.Domain.Users;

namespace CalcCal.Application.Users.GetCurrentUser;

public record UserSimpleModel
{
    public string UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }

    private UserSimpleModel(
        string userId,
        string firstName,
        string lastName,
        string phoneNumber)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    internal static UserSimpleModel FromDomainObject(User domainObject)
    {
        return new UserSimpleModel(
            domainObject.Id.Id.ToString(),
            domainObject.FirstName.Value,
            domainObject.LastName.Value,
            domainObject.PhoneNumber.Value);
    }
}