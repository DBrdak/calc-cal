namespace CalcCal.API.Endpoints.Users.Requests;

internal sealed record NewPasswordRequest(
    string CountryCode,
    string PhoneNumber,
    string VerificationCode,
    string NewPassword)
{
}