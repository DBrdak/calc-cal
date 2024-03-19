using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Users;
using CalcCal.Infrastructure.Repositories;
using CalcCal.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CalcCal.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            services.AddAuthentication(configuration);

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
    }
}
