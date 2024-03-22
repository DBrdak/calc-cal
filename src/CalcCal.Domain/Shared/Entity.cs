﻿using Exceptions.DB;

namespace CalcCal.Domain.Shared;

public abstract class Entity{}

public abstract class Entity<TId> : Entity 
    where TId : EntityId, new()
{
    public TId Id { get; set;}

    protected Entity(TId id) => Id = id;

    protected Entity(Guid id)
    {
        Id = Activator.CreateInstance(typeof(TId), id) as TId ??
             throw new DomainException<Entity<TId>>($"Cannot create id of type {typeof(TId)} from value {id}");
    }

    protected Entity(string id)
    {
        Id = Activator.CreateInstance(typeof(TId), id) as TId ??
             throw new DomainException<Entity<TId>>($"Cannot create id of type {typeof(TId)} from value {id}");
    }

    protected Entity()
    {
    }
}