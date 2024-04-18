using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CalcCal.Infrastructure.Authorization.PhoneVerifiedRequirement;

public sealed class PhoneVerifiedRequirement : IAuthorizationRequirement
{
    public static readonly string PolicyName = nameof(PhoneVerifiedRequirement);
}