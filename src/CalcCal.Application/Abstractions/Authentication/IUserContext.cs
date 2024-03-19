using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Application.Abstractions.Authentication
{
    public interface IUserContext
    {
        string UserId { get; }
        Result<string> TryGetUserId();
    }
}
