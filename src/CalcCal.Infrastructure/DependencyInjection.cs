using System.Net;
using Microsoft.Extensions.Configuration;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Users;
using CalcCal.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Application.Abstractions.LLM;
using CalcCal.Infrastructure.Authentication;
using CalcCal.Infrastructure.LLM;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using CalcCal.Infrastructure.LLM.Gemini;
using CalcCal.Infrastructure.Phone;
using CalcCal.Infrastructure.Phone.SmsGateway;
using System.Net.Http;

namespace CalcCal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence();

        services.AddAuthentication();

        services.AddLLM(configuration);

        services.AddSmsGateway();
        
        return services;
    }

    private static void AddPersistence(this IServiceCollection services)
    {
        services.AddDataProtection();
        services.AddScoped<DbContext>();
        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddAuthentication(this IServiceCollection services)
    {
        services.ConfigureOptions<AuthenticationOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddAuthorization();

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();

        services.AddTransient<IPasswordService, PasswordService>();

        services.AddScoped<IJwtService, JwtService>();
    }

    private static void AddLLM(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GeminiOptions>(configuration.GetSection("Gemini"));

        services.AddTransient<GeminiDelegatingHandler>();

        services.AddHttpClient<GeminiClient>(
            (serviceProvider, httpClient) =>
            {
                var geminiOptions = serviceProvider.GetRequiredService<IOptions<GeminiOptions>>().Value;

                httpClient.BaseAddress = new Uri(geminiOptions.Url);
            })
            .AddHttpMessageHandler<GeminiDelegatingHandler>();

        services.AddScoped<ILLMService, LLMService>();
    }

    private static void AddSmsGateway(this IServiceCollection services)
    {
        void ConfigureHttpClient(IServiceProvider serviceProvider, HttpClient client)
        {
            var options = serviceProvider.GetRequiredService<IOptions<SmsGatewayOptions>>().Value;

            var formattedUrl = options.Url.Remove(
                options.Url.IndexOf("//", StringComparison.Ordinal) + 2,
                options.Url.LastIndexOf('@') - options.Url.IndexOf("//", StringComparison.Ordinal) - 1);

            client.BaseAddress = new Uri(formattedUrl);
        }

        services.ConfigureOptions<SmsGatewayOptionsSetup>();

        services.AddTransient<SmsGatewayDelegatingHandler>();

        services.AddHttpClient<SmsGatewayClient>(ConfigureHttpClient)
                .AddHttpMessageHandler<SmsGatewayDelegatingHandler>();

        services.AddScoped<IPhoneService, PhoneService>();
    }
}