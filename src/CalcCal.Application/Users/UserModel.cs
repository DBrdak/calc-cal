using CalcCal.Domain.Users;

namespace CalcCal.Application.Users;

public sealed record UserModel
{
    public string UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
    public List<EatenFoodModel> EatenFood { get; init;}

    private UserModel(
        string userId,
        string firstName,
        string lastName,
        string phoneNumber,
        List<EatenFoodModel> eatenFood)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        EatenFood = eatenFood;
    }

    internal static UserModel FromDomainObject(User domainObject)
    {
        return new UserModel(
            domainObject.Id.Id.ToString(),
            domainObject.FirstName.Value,
            domainObject.LastName.Value,
            domainObject.PhoneNumber.Value,
            domainObject.EatenFood.Select(EatenFoodModel.FromDomainObject).ToList());
    }
}