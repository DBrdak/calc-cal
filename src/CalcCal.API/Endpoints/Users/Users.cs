using System.Net;
using CalcCal.API.Endpoints.Users.Requests;
using CalcCal.API.Extensions;
using CalcCal.Application.Models;
using CalcCal.Application.Users.GetCurrentUser;
using CalcCal.Application.Users.GetUserDetails;
using CalcCal.Application.Users.LogIn;
using CalcCal.Application.Users.RegisterUser;
using Carter;
using MediatR;
using Responses.DB;

namespace CalcCal.API.Endpoints.Users;

public sealed class Users : ICarterModule
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
                })
            .RequireAuthorization()
            .RequireRateLimiting(RateLimiterPolicies.FixedLoose);

        app.MapPost(
                "api/users/login",
                async (ISender sender, LogInRequest request, CancellationToken cancellationToken) =>
                {
                    var command = new LogInCommand(request.Username, request.Password);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess
                        ? Results.Ok(result.Value)
                        : Results.BadRequest(result.Error);
                })
            .RequireRateLimiting(RateLimiterPolicies.FixedStandard);

        app.MapPost(
                "api/users/register",
                async (ISender sender, RegisterRequest request, CancellationToken cancellationToken) =>
                {
                    var command = new RegisterUserCommand(
                        request.Username,
                        request.FirstName,
                        request.LastName,
                        request.CountryCode,
                        request.PhoneNumber,
                        request.Password);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess
                        ? Results.Ok()
                        : Results.BadRequest(result.Error);
                })
            .RequireRateLimiting(RateLimiterPolicies.FixedStandard);
    }
}