namespace CalcCal.Application.Food;

public sealed record FoodModel
{
    public string Name { get; init; }
    public decimal Calories { get; init; }

    private FoodModel(string name, decimal calories)
    {
        Name = name;
        Calories = calories;
    }

    internal static FoodModel FromDomainObject(Domain.Foods.Food domainObject)
    {
        return new FoodModel(domainObject.Name.Value, domainObject.Caloriers.Value);
    }
}