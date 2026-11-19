namespace MassTransitSaga.Contracts.Events;

public record EmailSentRabbitMQEvent(
    Guid ProductId,
    string Name,
    string Description
);