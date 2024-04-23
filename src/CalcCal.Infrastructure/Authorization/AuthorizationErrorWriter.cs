using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Responses.DB;

namespace CalcCal.Infrastructure.Authorization;

internal sealed class AuthorizationErrorWriter(IHttpContextAccessor httpContextAccessor)
{
    internal async Task Write(Error error)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            return;
        }

        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        await httpContext.Response.WriteAsJsonAsync(Result.Failure(error));
    }
}