using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Foods
{
    public sealed class Food : Entity<FoodId>
    {
        public FoodName Name { get; init; }
        public Calories Caloriers { get; private set; }
        public DateTime PublishedOn { get; init; }
        public DateTime LastEatenOn { get; private set; }
        public int EatCount { get; private set; }

        public Food()
        { }

        private Food(FoodName name, Calories caloriers) : base(new FoodId())
        {
            Name = name;
            Caloriers = caloriers;
            PublishedOn = DateTime.UtcNow;
            LastEatenOn = DateTime.UtcNow;
            EatCount = 1;
        }

        public static Result<Food> Create(string name, decimal calories)
        {
            var nameResult = FoodName.Create(name);

            if (nameResult.IsFailure)
            {
                return Result.Failure<Food>(nameResult.Error);
            }
            
            var caloriesResult = Calories.Create(calories);

            if (caloriesResult.IsFailure)
            {
                return Result.Failure<Food>(caloriesResult.Error);
            }

            return new Food(nameResult.Value, caloriesResult.Value);
        }

        public void View()
        {
            LastEatenOn = DateTime.UtcNow;
            EatCount++;
        }
    }
}
