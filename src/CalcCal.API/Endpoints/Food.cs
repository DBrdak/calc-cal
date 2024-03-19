using CalcCal.API.Extensions;
using CalcCal.Application.Food.GetAllFood;
using Carter;
using MediatR;

namespace CalcCal.API.Endpoints
{
    public class Food : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/food",
                async (ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetAllFoodQuery(), cancellationToken);

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
}
