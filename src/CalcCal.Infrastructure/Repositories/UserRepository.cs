using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Users;
using MongoDB.Driver;
using Responses.DB;

namespace CalcCal.Infrastructure.Repositories
{
    public sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<Result<User>> GetUserById(UserId id, CancellationToken cancellationToken)
        {
            var cursor = await Context.Set<User>().FindAsync(u => u.Id == id, null, cancellationToken);

            var user = await cursor.FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                Result.Failure<User>(Error.NotFound("User not found"));
            }

            return user;
        }
    }
}
