using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Shared;

namespace CalcCal.Domain.Users.DomainEvents;

public sealed record UserRegisteredDomainEvent(UserId UserId) : IDomainEvent
{
}