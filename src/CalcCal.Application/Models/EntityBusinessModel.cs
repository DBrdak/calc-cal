using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CalcCal.Domain.Shared;

namespace CalcCal.Application.Models;

public abstract record EntityBusinessModel
{
    private readonly List<IDomainEvent> _domainEvents;

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents;

    protected EntityBusinessModel(IEnumerable<IDomainEvent> domainEvents)
    {
        _domainEvents = domainEvents.ToList();
    }
}