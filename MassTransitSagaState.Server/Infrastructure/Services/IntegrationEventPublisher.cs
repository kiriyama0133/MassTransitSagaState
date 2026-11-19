using MassTransit;
using MassTransitSagaState.Server.Application.Interfaces;

namespace MassTransitSagaState.Server.Infrastructure.Services;


public class IntegrationEventPublisher(IBus bus) : IIntegrationEventPublisher
{
    public Task Publish<T>(T message) where T : class
    {
        return bus.Publish(message);
    }
}
