using System.Security.Claims;
using CalcCal.Application.Abstractions.Authentication;
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
            .SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?
            .Value ??
        throw new ApplicationException("User context is unavailable");

    public Result<string> TryGetUserId()
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