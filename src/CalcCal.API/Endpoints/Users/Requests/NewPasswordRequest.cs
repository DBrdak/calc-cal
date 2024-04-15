namespace CalcCal.API.Endpoints.Users.Requests
{
    internal sealed record NewPasswordRequest(
        string? Username,
        string? CountryCode,
        string? PhoneNumber,
        string NewPassword)
    {
    }
}
