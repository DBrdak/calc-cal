using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Messaging;

namespace CalcCal.Application.Users.VerifyCode
{
    public sealed record VerifyCodeCommand(string Code) : ICommand
    {
    }
}
