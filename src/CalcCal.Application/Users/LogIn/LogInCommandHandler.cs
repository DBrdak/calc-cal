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

        var getUserResult = await _userRepository.GetUserByUsername(usernameResult.Value, cancellationToken);

        if (getUserResult.IsFailure)
        {
            return Result.Failure<AccessToken>(Error.InvalidRequest($"User with username: {request.Username} does not exist"));
        }

        var result = await _jwtService.GetAccessTokenAsync(
            getUserResult.Value,
            request.Password,
            cancellationToken);

        var user = getUserResult.Value;
        user.LogIn();

        var userUpdateResult = await _userRepository.Update(user, cancellationToken);

        if (userUpdateResult.IsFailure)
        {
            return Result.Failure<AccessToken>(userUpdateResult.Error);
        }

        return result.IsFailure
            ? Result.Failure<AccessToken>(UserErrors.InvalidCredentials)
            : new AccessToken(result.Value);
    }
}