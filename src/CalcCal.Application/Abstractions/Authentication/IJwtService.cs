using CalcCal.Domain.Users;
using Responses.DB;

namespace CalcCal.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        User userToAuthenticate,
        string password,
        CancellationToken cancellationToken = default);
}