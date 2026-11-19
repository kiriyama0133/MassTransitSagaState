using MassTransit;
using MassTransitSaga.Contracts.Events;

namespace MassTransitSagaState.EmailPage.Worker.Handlers;

public class EmailSentIntegrationEventConsumer(ILogger<EmailSentIntegrationEventConsumer> logger)
    : IConsumer<EmailSentRabbitMQEvent>
{
    public Task Consume(ConsumeContext<EmailSentRabbitMQEvent> context)
    {
        logger.LogInformation(
            "Integration event received: EmailSent for product {ProductId}",
            context.Message.ProductId);

        return Task.CompletedTask;
    }
}
