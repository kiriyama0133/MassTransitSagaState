using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Hosting.Contracts;
using Microsoft.Extensions.Hosting.Domain.Entities;
using Microsoft.Extensions.Hosting.Infrastructure.Persistence;

namespace MassTransitSagaState.Server.Handler;

public class CreateProductHandler(IScopedMediator mediator) 
    : IConsumer<CreateProductEvent>
{
    public async Task Consume(ConsumeContext<CreateProductEvent> context)
    {
        var product = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = context.Message.Name,
            Description =  context.Message.Description,
        };

        await mediator.Publish(new ProductCreated(
            product.Id,
            product.Name,
            product.Description,
            DateTime.UtcNow
        ));
    }
}