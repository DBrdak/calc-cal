using CalcCal.Domain.Users;
using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Username,
    string FirstName,
    string LastName,
    string CountryCode,
    string PhoneNumber,
    string Password) : ICommand<User>
{
}