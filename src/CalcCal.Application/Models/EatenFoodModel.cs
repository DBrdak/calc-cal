using CalcCal.Domain.Foods;
using CalcCal.Domain.Shared;
using CalcCal.Domain.Users;

namespace CalcCal.Application.Models;

public sealed record EatenFoodModel
{
    public string FoodName { get; init; }
    public decimal GramsQuantity { get; init; }
    public decimal CaloriesEaten { get; init; }
    public DateTime EatenDateTime { get; init; }

    private EatenFoodModel(
        string FoodName,
        decimal GramsQuantity,
        decimal caloriesEaten,
        DateTime eatenDateTime)
    {
        this.FoodName = FoodName;
        this.GramsQuantity = GramsQuantity;
        CaloriesEaten = caloriesEaten;
        EatenDateTime = eatenDateTime;
    }

    internal static EatenFoodModel FromDomainObject(EatenFood domainObject)
    {
        return new EatenFoodModel(
            domainObject.FoodName.Value,
            domainObject.Quantity.Value,
            domainObject.CaloriesEaten.Value,
            domainObject.EatenDateTime);
    }
}