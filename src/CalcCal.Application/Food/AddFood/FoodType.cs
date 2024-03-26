using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Exceptions.DB;
using Responses.DB;

namespace CalcCal.Application.Food.AddFood
{
    public sealed record FoodType
    {
        public string Value { get; init; }

        public static readonly FoodType Dish = new ("Dish");
        public static readonly FoodType Product = new("Product");
        private static readonly Error validationError = new Error(
            "FoodType.ValidationError",
            "Wrong value of food type was provided");

        private FoodType(string value)
        {
            Value = value;
        }

        public static Result<FoodType> FromString(string value) =>
            All.FirstOrDefault(x => x.Value.ToLower() == value.ToLower()) ??
            Result.Failure<FoodType>(validationError);


        public static IReadOnlyCollection<FoodType> All = new[]
        {
            Dish,
            Product,
        };
    }
}
