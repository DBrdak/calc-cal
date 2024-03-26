using CalcCal.Domain.Foods;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace CalcCal.Application.Food.GetFood;

internal sealed class GetFoodQueryHandler : IQueryHandler<GetFoodQuery, IEnumerable<FoodModel>>
{
    private readonly IFoodRepository _foodRepository;

    public GetFoodQueryHandler(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }


    public async Task<Result<IEnumerable<FoodModel>>> Handle(GetFoodQuery request, CancellationToken cancellationToken)
    {
        var foodResult = request.FoodName is null
                ? await _foodRepository.GetFood(cancellationToken)
                : await _foodRepository.GetFood(request.FoodName, cancellationToken);

        return foodResult.IsFailure ?
            Result.Failure<IEnumerable<FoodModel>>(foodResult.Error) :
            Result.Create(foodResult.Value.Select(FoodModel.FromDomainObject));
    }

}