using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Application.Abstractions.LLM;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Users;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace CalcCal.Application.Food.AddFood
{
    internal sealed class AddFoodCommandHandler : ICommandHandler<AddFoodCommand, IEnumerable<FoodModel>>
    {
        private readonly ILLMService _llmService;
        private readonly IFoodRepository _foodRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public AddFoodCommandHandler(
            ILLMService llmService,
            IFoodRepository foodRepository,
            IUserRepository userRepository,
            IUserContext userContext)
        {
            _llmService = llmService;
            _foodRepository = foodRepository;
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task<Result<IEnumerable<FoodModel>>> Handle(AddFoodCommand request, CancellationToken cancellationToken)
        {
            var foodResult = await _foodRepository.GetFood(request.FoodName, cancellationToken);

            if (foodResult.IsFailure)
            {
                return Result.Failure<IEnumerable<FoodModel>>(foodResult.Error);
            }

            if (foodResult.Value.Any())
            {
                return Result.Create(foodResult.Value.Select(FoodModel.FromDomainObject));
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

            var foodBuilder = new FoodDataBuilder();
            var foodBuildResult = foodBuilder.CreateFromLLMResponse(llmResult.Value);

            if (foodBuildResult.IsFailure)
            {
                return Result.Failure<IEnumerable<FoodModel>>(foodBuildResult.Error);
            }

            var food = foodBuilder.Food;

            var addFoodResult = await _foodRepository.Add(food, cancellationToken);

            if (addFoodResult.IsFailure)
            {
                return Result.Failure<IEnumerable<FoodModel>>(addFoodResult.Error);
            }

            return new [] { FoodModel.FromDomainObject(food) };
        }

    }
}
