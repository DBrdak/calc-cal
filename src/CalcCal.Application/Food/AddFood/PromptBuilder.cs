using Responses.DB;

namespace CalcCal.Application.Food.AddFood
{
    internal sealed class PromptBuilder
    {
        private Prompt? _getFoodPrompt = null;
        private string foodName;

        public PromptBuilder(string foodName)
        {
            this.foodName = foodName;
        }

        public Prompt GetFoodPrompt => _getFoodPrompt ??
                                       throw new InvalidOperationException($"Cannot access null value of {nameof(_getFoodPrompt)}");

        public Result BuildGetFoodPrompt()
        {
            var promptResult = Prompt.Create(
                $"""
                 Below you will get a input from user, you have to validate this input and if it's valid pass calorie content, there are 3 rules of validation:
                 1. Decide whether it is food or not.
                 2. User can pass input which can be a dish from concrete restaurant, if given dish is not served in given restaurant, you have to reject this input.
                 3. You also have to reject input when you can't give the calorie content of the food in answer

                 If validation fails you have 3 answer patterns, each for case:
                 1. Input isn't a food
                 2. Given food isn't served in given restaurant
                 3. Calorie content of this dish isn't available

                 Pattern of your answer:
                 If is valid: [Adjusted name of dish with location if provided] - [product or dish] - [number] kcal/[dish weight or 100g]
                 If is invalid: Invalid - reason of invalidation

                 There are some example cases:

                 User: Carrot
                 You: Carrot - Product - 41 kcal/100g
                 
                 User: Zinger from KFC in Warsaw
                 You: Zinger, KFC, Warsaw - Dish - 425 kcal/200g

                 User: Stone
                 You: Invalid - Input isn't a food

                 User: Big Mac in KFC - Warsaw
                 You: Invalid - Given food isn't served in given restaurant

                 Validate this food:
                 User: Big Mac w makdonaldzie w złotych tarasach
                 You: [answer pattern: Valid pattern (if valid) Invalid pattern (if invalid)]

                 Answer in max few words
                 """);

            if (promptResult.IsFailure)
            {
                return Result.Failure(promptResult.Error);
            }

            _getFoodPrompt = promptResult.Value;

            return Result.Success();
        }
    }
}
