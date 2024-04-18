using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CalcCal.Infrastructure.Authorization;

internal sealed class AuthorizationPolicyProvider
{
    public static Action<AuthorizationOptions> Configure => AddPhoneVerifiedPolicy;

    private static void AddPhoneVerifiedPolicy(AuthorizationOptions configuration)
    {
        configuration.AddPolicy(
            PhoneVerifiedRequirement.PhoneVerifiedRequirement.PolicyName,
            builder => builder
                .AddRequirements(new PhoneVerifiedRequirement.PhoneVerifiedRequirement())
                .Build());
    }
}