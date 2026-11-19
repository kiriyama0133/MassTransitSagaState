using MassTransit;
using MassTransitSagaState.Server.Handler;

namespace MassTransitSagaState.Server.Extension;

public static class MassTransitExtension
{
    /// <summary>
    /// 出口发布的 bus
    /// 进程内的 mediator 管道
    /// </summary>
    public static IServiceCollection AddMassTransitService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var connectionString = configuration.GetConnectionString("messaging");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException(
                        $"RabbitMQ connection string is missing. " +
                        $"Environment variable exists: " +
                        $"{!string.IsNullOrWhiteSpace(
                            Environment.GetEnvironmentVariable("ConnectionStrings__messaging"))}");
                }

                cfg.Host(new Uri(connectionString));
                
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddMediator(cfg =>
        {
            cfg.AddConsumer<CreateProductHandler>();
        });

        return services;
    }
}
