using Responses.DB;

namespace CalcCal.Domain.Shared;

public sealed record Quantity : ValueObject<decimal>
{
    private static readonly Error invalidValueError = new Error("Quantity.InvalidValue", "Invalid quantity value");
    public const string UnitCode = "g";
    public const string Unit = "grams";
    private const int maxQuantity = 100_000;
    private const int minQuantity = 0;

    private Quantity(decimal value) : base(value)
    {
    }

    public static Result<Quantity> Create(decimal value)
    {
        if (value is < minQuantity or > maxQuantity)
        {
            return Result.Failure<Quantity>(invalidValueError);
        }

        return new Quantity(value);
    }

    public override string ToString() => $"{Value:##.#} {UnitCode}";
}