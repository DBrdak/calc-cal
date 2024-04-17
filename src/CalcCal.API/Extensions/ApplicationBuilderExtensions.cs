using System.Threading.RateLimiting;
using CalcCal.API.Middlewares;
using CalcCal.Application;
using CalcCal.Infrastructure;
using Carter;
using HealthChecks.ApplicationStatus.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace CalcCal.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection InjectDependencies(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        services.AddHealthChecks()
            .AddApplicationStatus()
            .AddMongoDb(configuration["Database:ConnectionString"] ?? string.Empty);

        services.AddRateLimiters();

        services.AddInfrastructure(configuration);

        services.AddApplication();

        services.AddCarter();

        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("DefaultPolicy",
                builder =>
                {
                    builder.WithOrigins(
                            "http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
        });

        return services;
    }

    private static void AddRateLimiters(this IServiceCollection services)
    {
        services.AddRateLimiter(
            options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddPolicy(
                    RateLimiterPolicies.FixedLoose,
                    context =>
                        RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: context.Connection.RemoteIpAddress?.ToString(),
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 50,
                                Window = TimeSpan.FromSeconds(10)
                            }));
                options.AddPolicy(
                    RateLimiterPolicies.FixedStrict,
                    context =>
                        RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: context.Connection.RemoteIpAddress?.ToString(),
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 5,
                                Window = TimeSpan.FromSeconds(10)
                            }));
                options.AddPolicy(
                    RateLimiterPolicies.FixedStandard,
                    context =>
                        RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: context.Connection.RemoteIpAddress?.ToString(),
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 10,
                                Window = TimeSpan.FromSeconds(10)
                            }));

                options.AddPolicy(
                    RateLimiterPolicies.UserOnePerHour,
                    context =>
                        RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: context.User.Identity?.Name?.ToString(),
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 1,
                                Window = TimeSpan.FromHours(1)
                            }));
                options.AddPolicy(
                    RateLimiterPolicies.UserOnePerMinute,
                    context =>
                        RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: context.User.Identity?.Name?.ToString(),
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 1,
                                Window = TimeSpan.FromMinutes(1)
                            }));
            });
    }

    public static void AddMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExpiredTokenMiddleware>();
        app.UseMiddleware<MonitoringMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();
    }

    public static void AddHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks(
            "/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
    }

    public static void SecureApp(this WebApplication app)
    {
        app.UseRateLimiter();
        app.UseXContentTypeOptions();
        app.UseReferrerPolicy(opt => opt.NoReferrer());
        app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
        app.UseXfo(opt => opt.Deny());
        app.UseCspReportOnly(opt => opt
            .BlockAllMixedContent()
            .StyleSources(s => s.Self()
                .CustomSources("https://fonts.googleapis.com")
                .UnsafeInline())
            .FontSources(s => s.Self()
                .CustomSources("https://fonts.gstatic.com", "data:"))
            .FormActions(s => s.Self())
            .FrameAncestors(s => s.Self())
            .ScriptSources(s => s.Self()));

        if (!app.Environment.IsDevelopment())
        {
            app.Use(
                async (context, next) =>
                {
                    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
                    await next.Invoke();
                });
        }
    }
}