using Microsoft.Extensions.DependencyInjection;
using CalcCal.Application.Behaviors;

namespace CalcCal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
                //config.AddOpenBehavior(typeof(DomainEventPublishBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

        return services;
    }
}