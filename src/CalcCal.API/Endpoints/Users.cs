using CalcCal.API.Extensions;
using CalcCal.Application.Users.GetCurrentUser;
using CalcCal.Application.Users.LogIn;
using CalcCal.Application.Users.RegisterUser;
using Carter;
using MediatR;

namespace CalcCal.API.Endpoints;

public class Users : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "api/users/current",
            async (ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetCurrentUserQuery();

                var result = await sender.Send(query, cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            }).RequireAuthorization().RequireRateLimiting(RateLimiterPolicies.FixedLoose);

        app.MapPost(
            "api/users/login",
            async (ISender sender, LogInCommand command, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(command, cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.Error);
            }).RequireRateLimiting(RateLimiterPolicies.FixedStandard);

        app.MapPost(
            "api/users/register",
            async (ISender sender, RegisterUserCommand command, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(command, cancellationToken);

                return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest(result.Error);
            }).RequireRateLimiting(RateLimiterPolicies.FixedStandard);
    }
}