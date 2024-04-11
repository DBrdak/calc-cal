using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;

namespace CalcCal.Application.Users.GetCurrentUser;

public sealed record GetCurrentUserQuery() : IQuery<UserDetailedModel>
{
}