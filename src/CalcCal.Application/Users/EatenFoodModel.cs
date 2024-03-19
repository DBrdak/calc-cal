using CalcCal.Domain.Users;

namespace CalcCal.Application.Users;

public sealed record EatenFoodModel
{
    public string Food { get; init; }
    public decimal Calories { get; init; }
    public decimal GramsQuantity { get; init; }

    private EatenFoodModel(string Food, decimal Calories, decimal GramsQuantity)
    {
        this.Food = Food;
        this.Calories = Calories;
        this.GramsQuantity = GramsQuantity;
    }

    internal static EatenFoodModel FromDomainObject(EatenFood domainObject)
    {
        return new EatenFoodModel(
            domainObject.Food.Name.Value,
            domainObject.Food.Caloriers.Value,
            domainObject.Quantity.Value);
    }
}