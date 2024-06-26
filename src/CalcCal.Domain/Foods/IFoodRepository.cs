﻿using Responses.DB;

namespace CalcCal.Domain.Foods;

public interface IFoodRepository
{
    Task<Result<List<Food>>> GetFood(string foodName, CancellationToken cancellationToken);

    Task<Result<List<Food>>> GetFood(CancellationToken cancellationToken);
    Task<Result<Food>> GetFoodById(FoodId foodId, CancellationToken cancellationToken);
    Task<Result<Food>> Add(Food food, CancellationToken cancellationToken);
    Task<Result<Food>> Update(Food food, CancellationToken cancellationToken);
}