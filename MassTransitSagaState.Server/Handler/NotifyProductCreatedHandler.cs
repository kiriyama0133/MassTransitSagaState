using MassTransit;
using MassTransit.Mediator;
using MassTransitSagaState.Server.Application.Interface;
using MassTransitSagaState.Server.Contracts.Email;
using Microsoft.Extensions.Hosting.Contracts;

namespace MassTransitSagaState.Server.Handler;

public class NotifyProductCreatedHandler(
    IEmailServer emailServer,
    ILogger<NotifyProductCreatedHandler> logger,
    IScopedMediator mediator
) 
    : IConsumer<CreateEmail>
{
    public async Task Consume(ConsumeContext<CreateEmail> context)
    {
        logger.Log(LogLevel.Information, "NotifyProductCreatedHandler.Handle");
        await emailServer.SendEmailAsync(context.Message.Name, context.Message.Description);
        await mediator.Publish(new EmailSent
        (
            context.Message.ProductId,
            context.Message.Name,
            context.Message.Description
        ));
    }
}