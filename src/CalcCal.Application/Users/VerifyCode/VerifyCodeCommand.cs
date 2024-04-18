using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;

namespace CalcCal.Application.Users.VerifyCode;

public sealed record VerifyCodeCommand(string Code, string CountryCode, string PhoneNumber, string VerificationType) : ICommand<UserDetailedModel>
{
}