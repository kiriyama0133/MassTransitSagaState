using MassTransit;
using MassTransitSaga.Contracts.Commands;
using MassTransitSaga.Contracts.Events;
using MassTransitSagaState.EmailPage.Worker.Services;

namespace MassTransitSagaState.EmailPage.Worker.Handlers;

public class SendEmailHandler(
    IEmailServer emailServer,
    ILogger<SendEmailHandler> logger)
    : IConsumer<CreateEmail>
{
    public async Task Consume(ConsumeContext<CreateEmail> context)
    {
        logger.LogInformation(
            "Sending email for product {ProductId}",
            context.Message.ProductId);

        await emailServer.SendEmailAsync(context.Message.Name, context.Message.Description);

        await context.Publish(new EmailSent(
            context.Message.ProductId,
            context.Message.Name,
            context.Message.Description));
    }
}
