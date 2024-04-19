using CalcCal.Application.Abstractions.LLM;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;
using CalcCal.Domain.Foods;
using MediatR;
using Responses.DB;

namespace CalcCal.Application.Food.AddFood;

internal sealed class AddFoodCommandHandler : ICommandHandler<AddFoodCommand, IEnumerable<FoodModel>>
{
    private readonly ILLMService _llmService;
    private readonly IFoodRepository _foodRepository;

    public AddFoodCommandHandler(
        ILLMService llmService,
        IFoodRepository foodRepository)
    {
        _llmService = llmService;
        _foodRepository = foodRepository;
    }

    public async Task<Result<IEnumerable<FoodModel>>> Handle(AddFoodCommand request, CancellationToken cancellationToken)
    {
        var foodResult = await GetExistingFoodAsync(request.FoodName, cancellationToken);

        if (foodResult is not null && foodResult.IsSuccess)
        {
            return Result.Create(foodResult.Value);
        }

        var promptBuilder = new PromptBuilder(request.FoodName);

        var promptBuildResult = promptBuilder.BuildGetFoodPrompt();

        if (promptBuildResult.IsFailure)
        {
            return Result.Failure<IEnumerable<FoodModel>>(promptBuildResult.Error);
        }

        var llmResult = await _llmService.SendPromptAsync(promptBuilder.GetFoodPrompt, cancellationToken);

        if (llmResult.IsFailure)
        {
            return Result.Failure<IEnumerable<FoodModel>>(llmResult.Error);
        }

        var responseProcessor = new LLMResponseProcessor();
        var responseProcessingResult = responseProcessor.ProcessLLMResponse(llmResult.Value);

        if (responseProcessingResult.IsFailure)
        {
            return Result.Failure<IEnumerable<FoodModel>>(responseProcessingResult.Error);
        }

        var food = responseProcessor.Food;

        foodResult = await GetExistingFoodAsync(food.Name.Value, cancellationToken);

        if (foodResult is not null && foodResult.IsSuccess)
        {
            return Result.Create(foodResult.Value);
        }

        var addFoodResult = await _foodRepository.Add(food, cancellationToken);

        if (addFoodResult.IsFailure)
        {
            return Result.Failure<IEnumerable<FoodModel>>(addFoodResult.Error);
        }

        return new List<FoodModel>() { FoodModel.FromDomainObject(food) };
    }

    private async Task<Result<IEnumerable<FoodModel>>?> GetExistingFoodAsync(string foodName, CancellationToken cancellationToken)
    {
        var foodResult = await _foodRepository.GetFood(foodName, cancellationToken);

        if (foodResult.Value.Any())
        {
            return Result.Create(foodResult.Value.Select(FoodModel.FromDomainObject));
        }

        return null;
    }
}