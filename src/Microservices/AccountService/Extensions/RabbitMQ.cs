namespace AccountService.Extensions;

public static class RabbitMQ
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetExecutingAssembly());

            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("localhost");
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(false));
            });
        });

        return services;
    }
}