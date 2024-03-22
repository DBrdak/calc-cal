using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Domain.Users;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace CalcCal.Application.Users.LogIn;

internal sealed class LogInCommandHandler : ICommandHandler<LogInCommand, AccessToken>
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;

    public LogInCommandHandler(IJwtService jwtService, IUserRepository userRepository)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
    }
    public async Task<Result<AccessToken>> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var usernameResult = Username.Create(request.Username);

        if (usernameResult.IsFailure)
        {
            return Result.Failure<AccessToken>(usernameResult.Error);
        }

        var user = await _userRepository.GetUserByUsername(usernameResult.Value, cancellationToken);

        if (user.IsFailure)
        {
            return Result.Failure<AccessToken>(Error.InvalidRequest($"User with username: {request.Username} does not exist"));
        }

        var result = await _jwtService.GetAccessTokenAsync(
            user.Value,
            request.Password,
            cancellationToken);

        return result.IsFailure
            ? Result.Failure<AccessToken>(UserErrors.InvalidCredentials)
            : new AccessToken(result.Value);
    }
}