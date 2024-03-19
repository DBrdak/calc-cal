using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Users.GetCurrentUser
{
    public sealed record GetCurrentUserQuery() : IQuery<UserModel>
    {
    }
}
