using MassTransit;
using MassTransitSagaState.Server.Contracts.Email;
using MassTransitSagaState.Server.Sagas;
using Microsoft.Extensions.Hosting.Contracts;

public class NewsletterNotifySaga
    : MassTransitStateMachine<NewsletterEmailSagaData>
{
    public State SendingEmailState { get; private set; } = null!;
    public State EmailSentState { get; private set; } = null!;

    public Event<ProductCreated> ProductCreated { get; private set; } = null!;
    public Event<EmailSent> EmailSent { get; private set; } = null!;

    public NewsletterNotifySaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => ProductCreated, e =>
            e.CorrelateById(m => m.Message.ProductId));

        Event(() => EmailSent, e =>
            e.CorrelateById(m => m.Message.ProductId));

        Initially(
            When(ProductCreated)
                .Then(context =>
                {
                    context.Saga.ProductId = context.Message.ProductId;
                    context.Saga.Name = context.Message.Name;
                    context.Saga.Description = context.Message.Description;
                })
                .TransitionTo(SendingEmailState)
                .Publish(context => new CreateEmail(
                    context.Message.ProductId,
                    context.Message.Name,
                    context.Message.Description))
        );

        During(SendingEmailState,
            When(EmailSent)
                .Then(context =>
                {
                    context.Saga.NotifyProductSend = true;
                })
                .TransitionTo(EmailSentState)
        );
    }
}