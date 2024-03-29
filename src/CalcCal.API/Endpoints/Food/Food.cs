using CalcCal.API.Endpoints.Food.Requests;
using CalcCal.API.Extensions;
using CalcCal.Application.Food.AddFood;
using CalcCal.Application.Food.EatFood;
using CalcCal.Application.Food.GetFood;
using CalcCal.Application.Models;
using Carter;
using MediatR;
using Responses.DB;

namespace CalcCal.API.Endpoints.Food
{
    public sealed class Food : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/food",
                async (ISender sender, string? foodName, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetFoodQuery(foodName), cancellationToken);
                    
                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.NotFound(result.Value);
                }).RequireRateLimiting(RateLimiterPolicies.FixedLoose);

            app.MapPost(
                "api/food",
                async (ISender sender, AddFoodRequest request, CancellationToken cancellationToken) =>
                {
                    var command = new AddFoodCommand(request.FoodName);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                }).RequireRateLimiting(RateLimiterPolicies.FixedStrict);

            app.MapPut(
                "api/food",
                async (ISender sender, EatFoodRequest request, CancellationToken cancellationToken) =>
                {
                    var command = new EatFoodCommand(request.FoodId, request.FoodWeight);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);

                }).RequireRateLimiting(RateLimiterPolicies.FixedLoose);
        }
    }
}
