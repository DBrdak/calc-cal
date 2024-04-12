using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;
using CalcCal.Domain.Users;

namespace CalcCal.Application.Users.ChangePassword
{
    public sealed record ChangePasswordCommand(
        string? Username,
        string? CountryCode,
        string? PhoneNumber,
        string NewPassword) : ICommand<UserDetailedModel> 
    {
    }
}
