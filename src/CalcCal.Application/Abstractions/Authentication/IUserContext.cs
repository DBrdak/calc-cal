using Responses.DB;

namespace CalcCal.Application.Abstractions.Authentication;

public interface IUserContext
{
    string UserId { get; }
    Result<string> GetUserId();
}