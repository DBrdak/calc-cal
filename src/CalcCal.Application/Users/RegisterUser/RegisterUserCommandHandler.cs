using System.Text.RegularExpressions;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Domain.Users;
using Responses.DB;

namespace CalcCal.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private const string passwordPattern = @"^(?=.*[!@#$%^&*()-_=+{};:',.<>?])(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<Result<User>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateUserAsync(
            request.CountryCode,
            request.PhoneNumber,
            request.Username,
            request.Password,
            cancellationToken);

        if (validationResult.IsFailure)
        {
            return (Result<User>)validationResult;
        }
            
        var passwordHash = _passwordService.HashPassword(request.Password);

        var userResult = User.Create(
            request.CountryCode,
            request.PhoneNumber,
            request.FirstName,
            request.LastName,
            request.Username,
            passwordHash);

        await _userRepository.Add(userResult.Value, cancellationToken);

        return userResult;
    }

    private async Task<Result> ValidateUserAsync(
        string countryCode,
        string phoneNumber,
        string username,
        string password,
        CancellationToken cancellationToken)
    {
        if (!Regex.IsMatch(password, passwordPattern))
        {
            return Result.Failure<User>(Error.InvalidRequest("Password is too weak"));
        }

        var users = await _userRepository.GetAllUsers(cancellationToken);

        if (users.IsFailure)
        {
            return Result.Failure<User>(users.Error);
        }

        var isPhoneNumberUnique = users.Value.All(u => u.PhoneNumber.Value != phoneNumber || u.PhoneNumber.CountryCode != countryCode);

        if (!isPhoneNumberUnique)
        {
            return Result.Failure<User>(Error.InvalidRequest($"PhoneNumber: {phoneNumber} is taken"));
        }

        var isUsernameUnique = users.Value.All(u => u.Username.Value.ToLower() != username.ToLower());

        if (!isUsernameUnique)
        {
            return Result.Failure<User>(Error.InvalidRequest($"Username: {username} is taken"));
        }

        return Result.Success();
    }
}