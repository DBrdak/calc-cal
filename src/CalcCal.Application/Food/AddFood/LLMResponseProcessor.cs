using Responses.DB;

// ReSharper disable InconsistentNaming

namespace CalcCal.Application.Food.AddFood
{
    internal sealed class LLMResponseProcessor
    {
        private string? foodName;
        private decimal foodWeight;
        private decimal foodCalories;
        private Domain.Foods.Food? _food;

        public Domain.Foods.Food Food =>
            _food ??
            throw new InvalidOperationException($"Cannot access null value of {nameof(_food)}");

        public Result ProcessLLMResponse(string response)
        {
            var dataArray = response.Split('-').Select(x => x.Trim()).ToArray();

            var validationResult = ValidateResponse(dataArray[0], dataArray[1], dataArray.Length);

            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            var processingResult = ProcessResponse(dataArray);

            if (processingResult.IsFailure)
            {
                return Result.Failure(processingResult.Error);
            }

            return CreateFood(IsDish(dataArray));
        }

        private Result ValidateResponse(string validationResult, string failureReason, int dataArrayLength)
        {
            var isValidationSuccessful = validationResult.ToLower() != "invalid" && dataArrayLength == 3;

            return isValidationSuccessful
                ? Result.Success()
                : Result.Failure(Error.TaskFailed(failureReason));
        }

        private Result ProcessResponse(string[] dataArray)
        {
            foodName = dataArray[0];
            var foodData = dataArray[2];

            if (!GetWeightFromFoodData(foodData) 
                || !GetCaloriesFromFoodData(foodData))
            {
                return Result.Failure(Error.TaskFailed("Error while proccessing external food data"));
            }

            return Result.Success();
        }

        private bool GetWeightFromFoodData(string data)
        {
            var weight = string.Concat(data.Split('/')[1].Split(' ')[0].TakeWhile(char.IsDigit));
            return decimal.TryParse(weight, out foodWeight);
        }

        private bool GetCaloriesFromFoodData(string data)
        {
            return decimal.TryParse(data.Split(' ')[0], out foodCalories);
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

            _food = foodResult.Value;

            return Result.Success();
        }

        private static bool IsDish(string[] dataArray) => dataArray[1].ToLower() == "dish";
    }
}
