using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Users.LogIn
{
    public sealed record LogInCommand(string Username, string Password) : ICommand<AccessToken>
    {
    }
}
