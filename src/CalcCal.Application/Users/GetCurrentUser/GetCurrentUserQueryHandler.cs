using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;
using CalcCal.Domain.Users;
using Responses.DB;

namespace CalcCal.Application.Users.GetCurrentUser;

internal sealed class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserDetailedModel>
{
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;

    public GetCurrentUserQueryHandler(IUserContext userContext, IUserRepository userRepository)
    {
        _userContext = userContext;
        _userRepository = userRepository;
    }

    public async Task<Result<UserDetailedModel>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUserById(new UserId(_userContext.UserId), cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<UserDetailedModel>(result.Error);
        }

        return UserDetailedModel.FromDomainObject(result.Value);
    }
}