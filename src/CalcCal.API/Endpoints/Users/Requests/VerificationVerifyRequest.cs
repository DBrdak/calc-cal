namespace CalcCal.API.Endpoints.Users.Requests;

internal sealed record VerificationVerifyRequest(string Code, string CountryCode, string PhoneNumber)
{
}