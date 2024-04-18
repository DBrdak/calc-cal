using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Infrastructure.Authorization;

internal sealed class AuthorizationErrors
{
    public static Error PhoneVerifiedAuthorizationError = new(
        "AuthorizationError.EmailVerifiedAuthorizationError",
        "Please verify phone number");
}