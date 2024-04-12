using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Messaging;

namespace CalcCal.Application.Users.ReSendVerificationCode
{
    public sealed record ReSendVerificationCodeCommand(string CountryCode, string PhoneNumber): ICommand
    {
    }
}
