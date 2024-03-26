using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

// ReSharper disable InconsistentNaming

namespace CalcCal.Application.Food.AddFood
{
    internal sealed class FoodDataBuilder
    {
        private string? foodName;
        private decimal foodWeight;
        private decimal foodCalories;
        private Domain.Foods.Food? _food;

        public Domain.Foods.Food Food =>
            _food ??
            throw new InvalidOperationException($"Cannot access null value of {nameof(_food)}");

        public Result CreateFromLLMResponse(string response)
        {
            var dataArray = response.Split('-').Select(x => x.Trim()).ToArray();

            var isValidationSuccessful = IsValidationSuccessful(dataArray);

            if (!isValidationSuccessful)
            {
                var validationFailureReason = dataArray[1];
                return Result.Failure(Error.TaskFailed(validationFailureReason));
            }

            var processResult = ProcessCalorieInfo(dataArray);

            if (processResult.IsFailure)
            {
                return Result.Failure(processResult.Error);
            }

            return CreateFood(IsDish(dataArray));
        }

        private Result CreateFood(bool isDish)
        {
            var foodResult = isDish switch
            {
                true => Domain.Foods.Food.CreateDish(foodName, foodCalories, foodWeight),
                false => Domain.Foods.Food.CreateProduct(foodName, foodCalories)
            };

            if (foodResult.IsFailure)
            {
                return Result.Failure(foodResult.Error);
            }

            Food = foodResult.Value;

            return Result.Success();
        }

        private Result ProcessCalorieInfo(string[] dataArray)
        {
            foodName = dataArray[0];

            if (!GetWeightFromCalorieInfo(dataArray[2]) 
                || !GetCaloriesFromCalorieInfo(dataArray[2]))
            {
                return Result.Failure(Error.TaskFailed("Error while proccessing external food data"));
            }

            return Result.Success();
        }

        private bool GetWeightFromCalorieInfo(string info)
        {
            var weight = string.Concat(info.Split(' ')[1].Split('/').TakeWhile(char.IsDigit));
            return decimal.TryParse(weight, out foodWeight);
        }

        private bool GetCaloriesFromCalorieInfo(string info)
        {
            return decimal.TryParse(info.Split(' ')[0], out foodCalories);
        }

        private static bool IsValidationSuccessful(string[] dataArray) =>
            dataArray[0].ToLower() != "invalid" && dataArray.Length == 3;

        private static bool IsDish(string[] dataArray) => dataArray[1].ToLower() == "dish";
    }
}
