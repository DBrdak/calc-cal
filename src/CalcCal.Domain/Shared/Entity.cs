using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Exceptions.DB;

namespace CalcCal.Domain.Shared;

public abstract class Entity
{
    
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity()
    {
    }

    protected Entity(List<IDomainEvent> domainEvents)
    {
        _domainEvents = domainEvents;
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents;

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() =>
        _domainEvents.Clear();
    
}

public abstract class Entity<TId> : Entity 
    where TId : EntityId, new()
{
    public TId Id { get; set; }

    protected Entity(TId id) : base(new List<IDomainEvent>())
    {
        Id = id;
    }

    protected Entity(Guid id) : base(new List<IDomainEvent>())
    {
        Id = Activator.CreateInstance(typeof(TId), id) as TId ??
             throw new DomainException<Entity<TId>>($"Cannot create id of type {typeof(TId)} from value {id}");
    }

    protected Entity(string id) : base(new List<IDomainEvent>())
    {
        Id = Activator.CreateInstance(typeof(TId), id) as TId ??
             throw new DomainException<Entity<TId>>($"Cannot create id of type {typeof(TId)} from value {id}");
    }

    protected Entity()
    {
        
    }
}