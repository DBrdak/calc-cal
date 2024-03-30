using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;

namespace CalcCal.Application.Users.GetUserDetails;

public sealed record GetUserDetailsQuery(string Username) : IQuery<UserDetailedModel>
{
}