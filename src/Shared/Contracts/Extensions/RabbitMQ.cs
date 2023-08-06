using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Contracts.Extensions;

public static class RabbitMQ
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetEntryAssembly());

            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("localhost");
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(false));
            });
        });

        return services;
    }
}