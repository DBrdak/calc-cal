using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Domain.Foods
{
    public interface IFoodRepository
    {
        Task<Result<List<Food>>> GetAllFood(CancellationToken cancellationToken);
        Task<Result<Food>> Add(Food food, CancellationToken cancellationToken);
        Task<Result<Food>> Update(Food food, CancellationToken cancellationToken);
    }
}
