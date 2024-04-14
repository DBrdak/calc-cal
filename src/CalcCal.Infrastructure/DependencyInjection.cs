﻿using System.Net;
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

namespace CalcCal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.AddAuthentication(configuration);

        services.AddLLM(configuration);

        services.AddSmsGateway(configuration);

        return services;
    }

    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataProtection();
        services.AddScoped<DbContext>();
        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
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

    private static void AddSmsGateway(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<SmsGatewayOptionsSetup>();
        services.AddScoped<IPhoneService, PhoneService>();
    }
}