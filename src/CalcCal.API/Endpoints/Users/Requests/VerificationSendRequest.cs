namespace CalcCal.API.Endpoints.Users.Requests
{
    internal sealed record VerificationSendRequest(string CountryCode, string PhoneNumber)
    {
    }
}
