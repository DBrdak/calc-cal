﻿using CalcCal.Domain.Foods;
using CalcCal.Domain.Shared;
using CalcCal.Domain.Users;

namespace CalcCal.Application.Models;

public sealed record EatenFoodModel
{
    public string Food { get; init; }
    public decimal Calories { get; init; }
    public decimal GramsQuantity { get; init; }
    public decimal CaloriesEaten { get; init; }
    public DateTime EatenDateTime { get; init; }

    private EatenFoodModel(
        string Food,
        decimal Calories,
        decimal GramsQuantity,
        decimal caloriesEaten,
        DateTime eatenDateTime)
    {
        this.Food = Food;
        this.Calories = Calories;
        this.GramsQuantity = GramsQuantity;
        CaloriesEaten = caloriesEaten;
        EatenDateTime = eatenDateTime;
    }

    internal static EatenFoodModel FromDomainObject(EatenFood domainObject)
    {
        return new EatenFoodModel(
            domainObject.Food.Name.Value,
            domainObject.Food.Caloriers.Value,
            domainObject.Quantity.Value,
            domainObject.CaloriesEaten.Value,
            domainObject.EatenDateTime);
    }
}