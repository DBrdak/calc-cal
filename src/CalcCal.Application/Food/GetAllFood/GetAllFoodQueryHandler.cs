using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Users;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace CalcCal.Application.Food.GetAllFood
{
    internal sealed class GetAllFoodQueryHandler : IQueryHandler<GetAllFoodQuery, List<Domain.Foods.Food>>
    {
        private readonly IFoodRepository _foodRepository;

        public GetAllFoodQueryHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }


        public async Task<Result<List<Domain.Foods.Food>>> Handle(GetAllFoodQuery request, CancellationToken cancellationToken)
        {
            return await _foodRepository.GetAllFood(cancellationToken);
        }
    }
}
