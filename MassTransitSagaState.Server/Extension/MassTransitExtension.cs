using MassTransit;
using MassTransitSagaState.Server.Handler;
using MassTransitSagaState.Server.Sagas;
using Microsoft.Extensions.Hosting.Infrastructure.Persistence;
using System.Reflection;

namespace MassTransitSagaState.Server.Extension;


public static class MassTransitExtension
{
    private static bool IsConsumerInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == typeof(IConsumer<>);
    }

    public static void AddMediatorConsumers(this IMediatorRegistrationConfigurator configurator, params Assembly[] assemblies)
    {
        var consumerType = assemblies
            .SelectMany(assemblies => assemblies.GetTypes())
            .Where(type => type is { IsClass: true, IsAbstract: false }
                && type.GetInterfaces().Any(IsConsumerInterface)
            )
            .Distinct()
            .ToArray();
        foreach (var consumer in consumerType)
        {
            configurator.AddConsumer(consumer);
        }
    }
    static public IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            //x.AddSagaStateMachine<NewsletterNotifySaga, NewsletterEmailSagaData>()
            //    .EntityFrameworkRepository(r =>
            //    {
            //        r.ExistingDbContext<AppDbContext>();
            //        r.UsePostgres();
            //    });
            x.UsingRabbitMq((context, cfg) =>
            {
                var connectionString =
                    configuration.GetConnectionString("messaging");

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
            cfg.AddMediatorConsumers(typeof(MassTransitException).Assembly);
            cfg.AddSagaStateMachine<NewsletterNotifySaga, NewsletterEmailSagaData>()
                .EntityFrameworkRepository(r =>
                {
                    r.ExistingDbContext<AppDbContext>();
                    r.UsePostgres();
                }
                );
        });
        return services;
    }

}
