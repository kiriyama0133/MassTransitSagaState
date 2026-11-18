using MassTransit;

namespace MassTransitSagaState.Server.Sagas;

public class NewsletterEmailSagaData : SagaStateMachineInstance 
{
    public Guid CorrelationId { get; set; }
}