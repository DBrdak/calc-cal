using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;

namespace CalcCal.Application.Users.SendVerificationCode;

public sealed record SendVerificationCodeCommand(string CountryCode, string PhoneNumber): ICommand<UserDetailedModel>
{
}