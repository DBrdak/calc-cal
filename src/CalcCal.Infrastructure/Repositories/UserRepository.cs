using CalcCal.Domain.Users;
using MongoDB.Driver;
using Responses.DB;

namespace CalcCal.Infrastructure.Repositories;

internal sealed class  UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }

    public async Task<Result<User>> GetUserById(UserId id, CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<User>().FindAsync(u => u.Id == id, null, cancellationToken);

        var user = await cursor.SingleOrDefaultAsync(cancellationToken);

        return user ?? Result.Failure<User>(Error.NotFound("User not found"));
    }

    public async Task<Result<User>> GetUserByUsername(Username username, CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<User>()
            .FindAsync(
                u => u.Username.Value.ToLower()
                    .Equals(username.Value.ToLower()),
                null,
                cancellationToken);

        var user = await cursor.SingleOrDefaultAsync(cancellationToken);

        return user ?? Result.Failure<User>(Error.NotFound("Users not found"));
    }

    public async Task<Result<List<User>>> GetAllUsers(CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<User>().FindAsync(FilterDefinition<User>.Empty, null, cancellationToken);

        var users = await cursor.ToListAsync(cancellationToken);

        return users ?? Result.Failure<List<User>>(Error.NotFound("Users not found"));
    }

    public async Task<Result<User>> GetUserByPhoneNumber(PhoneNumber phoneNumber, CancellationToken cancellationToken)
    {
        var cursor = await Context.Set<User>()
            .FindAsync(
                u => u.PhoneNumber == phoneNumber,
                null,
                cancellationToken);

        var user = await cursor.SingleOrDefaultAsync(cancellationToken);

        return user ?? Result.Failure<User>(Error.NotFound("User not found"));
    }
}