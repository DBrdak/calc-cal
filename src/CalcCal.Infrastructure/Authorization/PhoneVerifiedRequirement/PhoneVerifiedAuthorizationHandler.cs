using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Domain.Users;
using CalcCal.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authorization;

namespace CalcCal.Infrastructure.Authorization.PhoneVerifiedRequirement;

internal sealed class PhoneVerifiedAuthorizationHandler : AuthorizationHandler<PhoneVerifiedRequirement>
{
    private readonly AuthorizationErrorWriter _writer;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;

    public PhoneVerifiedAuthorizationHandler(AuthorizationErrorWriter writer, IUserContext userContext, IUserRepository userRepository)
    {
        _writer = writer;
        _userContext = userContext;
        _userRepository = userRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PhoneVerifiedRequirement requirement)
    {
        if (context.User.HasClaim(UserRepresentationModel.PhoneNumberVerifiedClaimName, true.ToString()) ||
            await IsPhoneNumberVerified())
        {
            context.Succeed(requirement);
            return;
        }

        await _writer.Write(AuthorizationErrors.PhoneVerifiedAuthorizationError);
        context.Fail();
    }

    private async ValueTask<bool> IsPhoneNumberVerified()
    {
        var getUserIdResult = _userContext.GetUserId();

        if (getUserIdResult.IsFailure)
        {
            return false;
        }

        var userId = new UserId(getUserIdResult.Value);
        var getUserResult = await _userRepository.GetUserById(userId, default);

        if (getUserResult.IsFailure)
        {
            return false;
        }

        var user = getUserResult.Value;

        return user.IsPhoneNumberVerified;
    }
}