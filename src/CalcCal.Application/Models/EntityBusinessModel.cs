using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CalcCal.Domain.Shared;

namespace CalcCal.Application.Models
{

    public abstract record EntityBusinessModel
    {
        [JsonIgnore]
        public List<IDomainEvent> DomainEvents { get; private set; }

        protected EntityBusinessModel(IEnumerable<IDomainEvent> domainEvents)
        {
            DomainEvents = domainEvents.ToList();
        }
    }
    
}
