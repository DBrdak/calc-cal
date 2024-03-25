using CalcCal.Domain.Foods;
using MongoDB.Bson;
using MongoDB.Driver;
using Responses.DB;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace CalcCal.Infrastructure.Repositories;

internal sealed class FoodRepository : Repository<Food, FoodId>, IFoodRepository
{
    public FoodRepository(DbContext context) : base(context)
    {
    }

    public async Task<Result<List<Food>>> GetFood(string foodName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(foodName) || foodName.Length < 3)
        {
            return new List<Food>();
        }

        var searchEngine = new SearchEngine(foodName);

        searchEngine.ApplySearch();

        var cursor = await Context.Set<Food>().FindAsync(searchEngine.Filter, searchEngine.Options, cancellationToken);

        var food = await cursor.ToListAsync(cancellationToken);

        if (food is null)
        {
            Result.Failure<Food>(Error.NotFound("Food not found"));
        }

        return food;
    }

    private sealed class SearchEngine(string searchPhrase)
    {
        public FilterDefinition<Food> Filter { get; private set; } = FilterDefinition<Food>.Empty;
        public FindOptions<Food> Options { get; private set; } = new();

        public void ApplySearch()
        {
            Filter = Builders<Food>.Filter.Regex(food => food.Name.Value, new BsonRegularExpression(searchPhrase, "i"));
            Options = new FindOptions<Food>
            {
                Sort = Builders<Food>.Sort.Ascending(food => food.Name.Value)
            };
        }
    }
}