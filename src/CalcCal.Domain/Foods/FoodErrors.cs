using Responses.DB;

namespace CalcCal.Domain.Foods;

internal static class FoodErrors
{
    public static readonly Error InvalidCalories = new Error(
        "Food.InvalidCalories",
        "Invalid value of calories");
    public static readonly Error InvalidName = new Error(
        "Food.InvalidName",
        "Invalid food name");
}