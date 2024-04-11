using Responses.DB;

namespace CalcCal.Application.Food.AddFood;

public sealed record Prompt
{
    public string Value { get; init; }
    private const int valueMaxLength = 1_500;
    private static readonly Error promptTooLongError = new(
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
            return Result.Failure<Prompt>(promptTooLongError);
        }

        return new Prompt(value);
    }
}