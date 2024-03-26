using CalcCal.Application.Users.GetCurrentUser;
using CalcCal.Domain.Users;

namespace CalcCal.Application.Users.GetUserDetails;

public sealed record UserDetailedModel : UserSimpleModel
{
    public List<EatenFoodModel> EatenFood { get; init; }

    private UserDetailedModel(
        string userId,
        string firstName,
        string lastName,
        string username,
        string phoneNumber,
        List<EatenFoodModel> eatenFood) :
        base(userId, firstName, lastName, username, phoneNumber)
    {
        EatenFood = eatenFood;
    }

    internal new static UserDetailedModel FromDomainObject(User domainObject)
    {
        return new UserDetailedModel(
            domainObject.Id.Id,
            domainObject.FirstName.Value,
            domainObject.LastName.Value,
            domainObject.Username.Value,
            domainObject.PhoneNumber.Value,
            domainObject.EatenFood.Select(EatenFoodModel.FromDomainObject).ToList());
    }
}