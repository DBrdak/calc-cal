using CalcCal.Domain.Foods;
using Responses.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcCal.Domain.Shared
{
    public sealed record Quantity : ValueObject<decimal>
    {
        private static readonly Error invalidValueError = new Error("Quantity.InvalidValue", "Invalid quantity value");
        public const string UnitCode = "g";
        public const string Unit = "grams";

        private Quantity(decimal value) : base(value)
        {
        }

        public static Result<Quantity> Create(decimal value)
        {
            if (value is < 0 or > 100_000)
            {
                return Result.Failure<Quantity>(invalidValueError);
            }

            return new Quantity(value);
        }

        public override string ToString() => $"{Value:##.#} {UnitCode}";
    }
}
