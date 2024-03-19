using CalcCal.Domain.Shared;

namespace CalcCal.Domain.Users;

public record UserId : EntityId
{
    public UserId() : base(Guid.NewGuid())
    { }
}