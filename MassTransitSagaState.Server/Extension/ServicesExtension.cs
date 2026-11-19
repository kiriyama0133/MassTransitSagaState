using MassTransitSagaState.Server.Application.Interfaces;
using MassTransitSagaState.Server.Infrastructure.Services;

namespace MassTransitSagaState.Server.Extension;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventPublisher, IntegrationEventPublisher>();
        services.AddScoped<ICreateProduct, CreateProductServive>();
        return services;
    }
}
