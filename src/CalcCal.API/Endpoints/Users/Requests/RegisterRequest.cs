namespace CalcCal.API.Endpoints.Users.Requests
{
    internal sealed record RegisterRequest(
        string Username,
        string FirstName,
        string LastName,
        string CountryCode,
        string PhoneNumber,
        string Password)
    {
    }
}
