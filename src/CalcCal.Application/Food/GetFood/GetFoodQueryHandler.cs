using CalcCal.Domain.Foods;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace CalcCal.Application.Food.GetFood;

internal sealed class GetFoodQueryHandler : IQueryHandler<GetFoodQuery, List<Domain.Foods.Food>>
{
    private readonly IFoodRepository _foodRepository;

    public GetFoodQueryHandler(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }


    public async Task<Result<List<Domain.Foods.Food>>> Handle(GetFoodQuery request, CancellationToken cancellationToken)
    {
        return await _foodRepository.GetFood(request.FoodName, cancellationToken);
    }

}