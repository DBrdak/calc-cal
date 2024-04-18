namespace CalcCal.Infrastructure.Phone.SmsGateway.Models.BlowerioRequest;

internal sealed record BlowerioRequest(string To, string Message)
{
    public IEnumerable<KeyValuePair<string, string>> AsForm() =>
        new List<KeyValuePair<string, string>>
        {
            new ("to", To),
            new ("message", Message),
        };
}