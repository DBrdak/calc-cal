using CalcCal.Domain.Foods;
using MongoDB.Driver;
using Responses.DB;

namespace CalcCal.Infrastructure.Repositories;

public sealed class FoodRepository : Repository<Food, FoodId>, IFoodRepository
{
    public FoodRepository(DbContext context) : base(context)
    {
    }

    public async Task<Result<List<Food>>> GetAllFood(CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<Food>().FindAsync(FilterDefinition<Food>.Empty, null, cancellationToken);

        var food = await cursor.ToListAsync(cancellationToken);

        if (food is null)
        {
            Result.Failure<Food>(Error.NotFound("Food not found"));
        }

        return food;
    }
}