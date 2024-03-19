﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Infrastructure.Authentication
{
    public static class AuthenticationErrors
    {
        internal static Error UserCannotBeAccessed = new Error(
            "Authentication.UserCannotBeAccessed", "Cannot access the current user");

        internal static readonly Error AuthenticationFailed = new(
            "Authentication.AuthenticationFailed",
            "Failed to acquire access token do to authentication failure");
    }
}
