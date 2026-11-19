using MassTransitSagaState.Server.Application.Interfaces;
using MassTransitSagaState.Server.Infrastructure.Services;
using Microsoft.Extensions.Hosting.Infrastructure.Persistence;

namespace MassTransitSagaState.Server.Extension;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();
        services.AddScoped<ICreateProduct, CreateProductServive>();
        services.AddScoped<AppDbContext>();
        return services;
    }
}
