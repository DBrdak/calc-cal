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
            return Result.Failure<List<Food>>(Error.NotFound("Food not found"));
        }

        return Result.Success(food);
    }

    public async Task<Result<Food>> GetFoodById(FoodId foodId, CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<Food>().FindAsync(food => food.Id == foodId, null, cancellationToken);

        var food = cursor.FirstOrDefault(cancellationToken);

        if (food is null)
        {
            return Result.Failure<Food>(Error.NotFound("Food not found"));
        }

        return food;
    }

    public async Task<Result<List<Food>>> GetFood(CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<Food>()
            .FindAsync(
                FilterDefinition<Food>.Empty,
                new FindOptions<Food>{Sort = Builders<Food>.Sort.Ascending(f => f.Name.Value) },
                cancellationToken);
        var food = await cursor.ToListAsync(cancellationToken);

        if (food is null)
        {
            return Result.Failure<List<Food>>(Error.NotFound("Food not found"));
        }

        return Result.Success(food);
    }

    private sealed class SearchEngine(string searchPhrase)
    {
        public FilterDefinition<Food> Filter { get; private set; } = FilterDefinition<Food>.Empty;
        public FindOptions<Food> Options { get; private set; } = new();

        public void ApplySearch()
        {
            var regexFilter = Builders<Food>.Filter.Regex(
                food => food.Name.Value,
                new BsonRegularExpression(searchPhrase, "i"));

            //var textScoreProjection = Builders<Food>.Projection.MetaTextScore("score");

            Filter = regexFilter;
                     //& Builders<Food>.Filter.Text(searchPhrase, new TextSearchOptions { CaseSensitive = false });
            Options = new FindOptions<Food>
            {
                //Sort = Builders<Food>.Sort.MetaTextScore("score"),
                //Projection = textScoreProjection
                Sort = Builders<Food>.Sort.Descending(f => f.EatCount)
            };
        }
    }
}