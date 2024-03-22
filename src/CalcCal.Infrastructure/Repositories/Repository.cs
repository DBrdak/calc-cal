using CalcCal.Domain.Shared;
using MongoDB.Driver;
using Responses.DB;

namespace CalcCal.Infrastructure.Repositories;

public abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : EntityId, new()
{
    protected readonly DbContext Context;

    protected Repository(DbContext context)
    {
        Context = context;
    }

    public async Task<Result<TEntity>> Add(TEntity entity, CancellationToken cancellationToken)
    {
        await Context.Set<TEntity>().InsertOneAsync(entity, null, cancellationToken);

        return Result.Success(entity);
    }

    public async Task<Result> Remove(TEntityId entityId, CancellationToken cancellationToken)
    {
        var result = await Context.Set<TEntity>().DeleteOneAsync(e => e.Id == entityId, cancellationToken);

        return result.IsAcknowledged && result.DeletedCount > 0 ?
            Result.Success() :
            Result.Failure(Error.TaskFailed($"Problem while removing {nameof(TEntity)}"));
    }
    public async Task<Result<TEntity>> Update(TEntity entity, CancellationToken cancellationToken)
    {
        var result = await Context.Set<TEntity>().ReplaceOneAsync(
            e => e.Id == entity.Id,
            entity,
            new ReplaceOptions(),
            cancellationToken);

        return result.IsAcknowledged && result.ModifiedCount > 0 ?
            Result.Success(entity) :
            Result.Failure<TEntity>(Error.TaskFailed($"Problem while updating {nameof(TEntity)}"));
    }
}