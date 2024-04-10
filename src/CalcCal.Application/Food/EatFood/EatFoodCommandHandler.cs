using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Users;
using Responses.DB;

namespace CalcCal.Application.Food.EatFood;

internal sealed class EatFoodCommandHandler : ICommandHandler<EatFoodCommand, EatenFoodModel>
{
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;
    private readonly IFoodRepository _foodRepository;

    public EatFoodCommandHandler(IUserContext userContext, IUserRepository userRepository, IFoodRepository foodRepository)
    {
        _userContext = userContext;
        _userRepository = userRepository;
        _foodRepository = foodRepository;
    }

    public async Task<Result<EatenFoodModel>> Handle(EatFoodCommand request, CancellationToken cancellationToken)
    {
        Result<EatenFood> eatenFoodCreateResult;

        var getFoodResult = await _foodRepository.GetFoodById(new(request.FoodId), cancellationToken);

        if (getFoodResult.IsFailure)
        {
            return Result.Failure<EatenFoodModel>(getFoodResult.Error);
        }

        var food = getFoodResult.Value;

        var getUserIdResult = _userContext.GetUserId();

        if (getUserIdResult.IsFailure)
        {
            eatenFoodCreateResult = EatenFood.Create(food, request.FoodQuantity);

            if (eatenFoodCreateResult.IsFailure)
            {
                return Result.Failure<EatenFoodModel>(eatenFoodCreateResult.Error);
            }

            return EatenFoodModel.FromDomainObject(eatenFoodCreateResult.Value);
        }

        var userId = new UserId(getUserIdResult.Value);
        var getUserResult = await _userRepository.GetUserById(userId, cancellationToken);

        if (getUserResult.IsFailure)
        {
            return Result.Failure<EatenFoodModel>(getUserResult.Error);
        }

        var user = getUserResult.Value;

        user.Eat(food, request.FoodQuantity);

        var userUpdateResult = await _userRepository.Update(user, cancellationToken);

        if (userUpdateResult.IsFailure)
        {
            return Result.Failure<EatenFoodModel>(userUpdateResult.Error);
        }

        var foodUpdateResult = await _foodRepository.Update(food, cancellationToken);

        if (foodUpdateResult.IsFailure)
        {
            return Result.Failure<EatenFoodModel>(foodUpdateResult.Error);
        }

        eatenFoodCreateResult = EatenFood.Create(food, request.FoodQuantity);

        if (eatenFoodCreateResult.IsFailure)
        {
            return Result.Failure<EatenFoodModel>(eatenFoodCreateResult.Error);
        }

        return EatenFoodModel.FromDomainObject(eatenFoodCreateResult.Value);
    }
}