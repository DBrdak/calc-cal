using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CalcCal.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddPersistence(services, configuration);

            return services;
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IPasswordService, PasswordService>();
        }
    }
}
