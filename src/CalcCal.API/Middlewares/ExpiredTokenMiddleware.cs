namespace CalcCal.API.Middlewares
{
    public sealed class ExpiredTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public ExpiredTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                if (context.Response.Headers.ContainsKey("WWW-Authenticate") &&
                    context.Response.Headers["WWW-Authenticate"].ToString().Contains("expired"))
                {
                    context.Response.StatusCode = StatusCodes.Status419AuthenticationTimeout;
                    return;
                }
            }
        }
    }
}
