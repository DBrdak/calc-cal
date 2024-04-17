using CalcCal.Domain.Shared;
using CalcCal.Domain.Users;

namespace CalcCal.Application.Models;

public sealed record UserDetailedModel : UserSimpleModel
{
    public List<EatenFoodModel> EatenFood { get; init; }

    private UserDetailedModel(
        string userId,
        string firstName,
        string lastName,
        string username,
        string countryCode,
        string phoneNumber,
        bool isPhoneNumberVerified,
        List<EatenFoodModel> eatenFood,
        IEnumerable<IDomainEvent> domainEvents) : base(
        userId,
        firstName,
        lastName,
        username,
        countryCode,
        phoneNumber,
        isPhoneNumberVerified,
        domainEvents)
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
            domainObject.PhoneNumber.CountryCode,
            domainObject.PhoneNumber.Value,
            domainObject.IsPhoneNumberVerified,
            domainObject.EatenFood.Select(EatenFoodModel.FromDomainObject).ToList(),
            domainObject.GetDomainEvents());
    }
}