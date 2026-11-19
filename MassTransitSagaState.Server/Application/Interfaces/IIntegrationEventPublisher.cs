namespace MassTransitSagaState.Server.Application.Interfaces;

public interface IIntegrationEventPublisher
{
    Task Publish<T>(T message) where T : class;
}
