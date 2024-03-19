using CalcCal.Domain.Users;
using Responses.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Application.Abstractions.Authentication
{
    public interface IJwtService
    {
        Task<Result<string>> GetAccessTokenAsync(
            User userToAuthenticate,
            string password,
            CancellationToken cancellationToken = default);
    }
}
