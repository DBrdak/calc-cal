using CalcCal.Domain.Users;
using MongoDB.Driver;
using Responses.DB;

namespace CalcCal.Infrastructure.Repositories;

public sealed class  UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }

    public async Task<Result<User>> GetUserById(UserId id, CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<User>().FindAsync(u => u.Id == id, null, cancellationToken);

        var user = await cursor.SingleOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            Result.Failure<User>(Error.NotFound("User not found"));
        }

        return user;
    }

    public async Task<Result<User>> GetUserByUsername(Username username, CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<User>().FindAsync(u => u.Username.Equals(username), null, cancellationToken);

        var user = await cursor.SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            Result.Failure<User>(Error.NotFound("Users not found"));
        }

        return user;
    }

    public async Task<Result<List<User>>> GetAllUsers(CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<User>().FindAsync(FilterDefinition<User>.Empty, null, cancellationToken);

        var users = await cursor.ToListAsync(cancellationToken);

        if (users is null)
        {
            Result.Failure<User>(Error.NotFound("Users not found"));
        }

        return users;
    }
}