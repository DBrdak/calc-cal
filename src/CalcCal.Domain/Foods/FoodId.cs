using CalcCal.Domain.Shared;

namespace CalcCal.Domain.Foods;

public record FoodId : EntityId
{
    public FoodId() : base(Guid.NewGuid())
    { }
}