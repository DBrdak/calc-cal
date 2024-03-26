using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Application.Food.AddFood
{
    public sealed record Prompt
    {
        public string Value { get; init; }
        private const int valueMaxLength = 1_000;
        private static readonly Error _promptTooLongError = new(
            "Prompt.TooLongValue",
            "Prompt value too long");

        private Prompt(string value)
        {
            Value = value;
        }

        internal static Result<Prompt> Create(string value)
        {
            if (value.Length > valueMaxLength)
            {
                return Result.Failure<Prompt>(_promptTooLongError);
            }

            return new Prompt(value);
        }
    }
}
