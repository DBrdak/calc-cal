﻿using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Foods;

public sealed class Food : Entity<FoodId>
{
    public FoodName Name { get; init; }
    public Calories Caloriers { get; private set; }
    public Quantity Weight { get; init; }
    public DateTime PublishedOn { get; init; }
    public DateTime LastEatenOn { get; private set; }
    public int EatCount { get; private set; }

    public Food()
    {
    }

    private Food(FoodName name, Calories caloriers, Quantity weight) : base(new FoodId())
    {
        Name = name;
        Caloriers = caloriers;
        Weight = weight;
        PublishedOn = DateTime.UtcNow;
        LastEatenOn = DateTime.UtcNow;
        EatCount = 1;
    }

    public static Result<Food> CreateProduct(string name, decimal calories)
    {
        var nameResult = FoodName.Create(name);

        if (nameResult.IsFailure)
        {
            return Result.Failure<Food>(nameResult.Error);
        }
            
        var caloriesResult = Calories.Create(calories);

        if (caloriesResult.IsFailure)
        {
            return Result.Failure<Food>(caloriesResult.Error);
        }

        var quantity = Quantity.Create(100).Value;

        return new Food(nameResult.Value, caloriesResult.Value, quantity);
    }

    public static Result<Food> CreateDish(string name, decimal calories, decimal gramsQuantity)
    {
        var nameResult = FoodName.Create(name);

        if (nameResult.IsFailure)
        {
            return Result.Failure<Food>(nameResult.Error);
        }

        var caloriesResult = Calories.Create(calories);

        if (caloriesResult.IsFailure)
        {
            return Result.Failure<Food>(caloriesResult.Error);
        }

        var quantityResult = Quantity.Create(gramsQuantity);

        if (quantityResult.IsFailure)
        {
            return Result.Failure<Food>(quantityResult.Error);
        }

        return new Food(nameResult.Value, caloriesResult.Value, quantityResult.Value);
    }

    internal void Eat()
    {
        LastEatenOn = DateTime.UtcNow;
        EatCount++;
    }
}