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

    }
}
