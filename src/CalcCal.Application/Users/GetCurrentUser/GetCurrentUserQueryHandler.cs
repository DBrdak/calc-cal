using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Domain.Users;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace CalcCal.Application.Users.GetCurrentUser;

internal sealed class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserSimpleModel>
{
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;

    public GetCurrentUserQueryHandler(IUserContext userContext, IUserRepository userRepository)
    {
        _userContext = userContext;
        _userRepository = userRepository;
    }

    public async Task<Result<UserSimpleModel>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUserById(new UserId(_userContext.UserId), cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<UserSimpleModel>(result.Error);
        }

        return UserSimpleModel.FromDomainObject(result.Value);
    }
}