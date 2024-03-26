using System.Runtime.InteropServices;

namespace CalcCal.Application.Food;

public sealed record FoodModel
{
    public string FoodId { get; init; }
    public string Name { get; init; }
    public decimal Calories { get; init; }
    public decimal Weight { get; init; }

    private FoodModel(string foodId, string name, decimal calories, decimal weight)
    {
        FoodId = foodId;
        Name = name;
        Calories = calories;
        Weight = weight;
    }

    internal static FoodModel FromDomainObject(Domain.Foods.Food domainObject)
    {
        return new FoodModel(domainObject.Id.Id, domainObject.Name.Value, domainObject.Caloriers.Value, domainObject.Weight.Value);
    }
}