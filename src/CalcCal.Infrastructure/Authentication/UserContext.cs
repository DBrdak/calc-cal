﻿using System.Security.Claims;
using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Http;
using Responses.DB;

namespace CalcCal.Infrastructure.Authentication;

internal class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .SingleOrDefault(claim => claim.Type == UserRepresentationModel.IdClaimName)?
            .Value ??
        throw new ApplicationException("User context is unavailable");

    public Result<string> GetUserId()
    {
        try
        {
            return UserId;
        }
        catch (ApplicationException)
        {
            return Result.Failure<string>(AuthenticationErrors.UserCannotBeAccessed);
        }
    }
}