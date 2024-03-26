using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Users.GetUserDetails
{
    public sealed record GetUserDetailsQuery(string Username) : IQuery<UserModel>
    {
    }
}
