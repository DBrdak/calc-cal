using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Domain.Users
{
    public interface IUserRepository
    {
        Task<Result<User>> GetUserById(UserId id, CancellationToken cancellationToken);
        Task<Result<User>> Add(User user, CancellationToken cancellationToken);
        Task<Result<User>> Update(User user, CancellationToken cancellationToken);
        Task<Result> Remove(UserId id, CancellationToken cancellationToken);
    }
}
