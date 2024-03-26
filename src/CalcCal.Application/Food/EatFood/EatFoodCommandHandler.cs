using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Users;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace CalcCal.Application.Food.EatFood
{
    internal sealed class EatFoodCommandHandler : ICommandHandler<EatFoodCommand, FoodModel>
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

        public async Task<Result<FoodModel>> Handle(EatFoodCommand request, CancellationToken cancellationToken)
        {
            var getFoodResult = await _foodRepository.GetFoodById(new(request.FoodId), cancellationToken);

            if (getFoodResult.IsFailure)
            {
                return Result.Failure<FoodModel>(getFoodResult.Error);
            }

            var food = getFoodResult.Value;

            var getUserIdResult = _userContext.TryGetUserId();

            if (getUserIdResult.IsFailure)
            {
                return FoodModel.FromDomainObject(food);
            }

            var userId = new UserId(getUserIdResult.Value);
            var getUserResult = await _userRepository.GetUserById(userId, cancellationToken);

            if (getUserResult.IsFailure)
            {
                return Result.Failure<FoodModel>(getUserResult.Error);
            }

            var user = getUserResult.Value;

            user.Eat(food, request.FoodQuantity);

            var userUpdateResult = await _userRepository.Update(user, cancellationToken);

            if (userUpdateResult.IsFailure)
            {
                return Result.Failure<FoodModel>(userUpdateResult.Error);
            }

            var foodUpdateResult = await _foodRepository.Update(food, cancellationToken);

            if (foodUpdateResult.IsFailure)
            {
                return Result.Failure<FoodModel>(foodUpdateResult.Error);
            }

            return FoodModel.FromDomainObject(food);
        }
    }
}
