using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Users.GetUserDetails
{
    public sealed record GetUserDetailsQuery(string Username) : IQuery<UserDetailedModel>
    {
    }
}
