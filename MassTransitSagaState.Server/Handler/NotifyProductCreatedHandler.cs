using MassTransit;
using MassTransitSagaState.Server.Application.Interface;
using Microsoft.Extensions.Hosting.Contracts;

namespace MassTransitSagaState.Server.Handler;

public class NotifyProductCreatedHandler(IEmailServer emailServer, ILogger<NotifyProductCreatedHandler> logger) : IConsumer<ProductCreated>
{
    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        logger.Log(LogLevel.Information, "NotifyProductCreatedHandler.Handle");
        await emailServer.SendEmailAsync(context.Message.Name, context.Message.Description);
    }
}