using CalcCal.Domain.Users;

namespace CalcCal.Application.Users.GetCurrentUser;

public record UserSimpleModel
{
    public string UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Username { get; init; }
    public string PhoneNumber { get; init; }

    protected UserSimpleModel(
        string userId,
        string firstName,
        string lastName,
        string username,
        string phoneNumber)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        PhoneNumber = phoneNumber;
    }

    internal static UserSimpleModel FromDomainObject(User domainObject)
    {
        return new UserSimpleModel(
            domainObject.Id.Id,
            domainObject.FirstName.Value,
            domainObject.LastName.Value,
            domainObject.Username.Value,
            domainObject.PhoneNumber.Value);
    }
}