using CalcCal.API.Extensions;
using CalcCal.Application.Food.GetFood;
using Carter;
using MediatR;

namespace CalcCal.API.Endpoints;

public class Food : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "api/food",
            async (ISender sender, string? foodName, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetFoodQuery(foodName ?? string.Empty), cancellationToken);

                return result.IsSuccess ?
                    Results.Ok(result.Value) :
                    Results.NotFound(result.Value);
            }).RequireRateLimiting(RateLimiterPolicies.FixedLoose);

        app.MapPost(
            "api/food",
            async () =>
            {

            }).RequireRateLimiting(RateLimiterPolicies.FixedStrict);
    }
}