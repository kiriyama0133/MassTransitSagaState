using MassTransit;

namespace MassTransitSaga.Sagas;

public class NewsletterEmailSagaData : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool NotifyProductSend { get; set; }
}
