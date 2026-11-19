using MassTransit;
using MassTransitSaga.Contracts.Commands;
using MassTransitSaga.Contracts.Events;

namespace MassTransitSaga.Sagas;

/// <summary>
/// 跨服务的编排者：商品创建后发邮件，邮件发完收尾。
/// 只消费跨进程消息，不接触任何业务 DbContext。
/// </summary>
public class NewsletterNotifySaga : MassTransitStateMachine<NewsletterEmailSagaData>
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
                .Then(async context =>
                {
                    context.Saga.ProductId = context.Message.ProductId;
                    context.Saga.Name = context.Message.Name;
                    context.Saga.Description = context.Message.Description;

                    // 通过当前消费上下文发布命令，走 bus 而不是进程内的 mediator
                    await context.Publish(new CreateEmail(
                        context.Message.ProductId,
                        context.Message.Name,
                        context.Message.Description));
                })
                .TransitionTo(SendingEmailState)
        );

        During(SendingEmailState,
            When(EmailSent)
                .Then(async context =>
                {
                    context.Saga.NotifyProductSend = true;

                    await context.Publish(new EmailSentRabbitMQEvent(
                        context.Message.ProductId,
                        context.Message.Name,
                        context.Message.Description));
                })
                .TransitionTo(EmailSentState)
        );
    }
}
