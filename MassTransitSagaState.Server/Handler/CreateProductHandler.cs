using MassTransit;
using MassTransitSaga.Contracts.Events;
using MassTransitSagaState.Server.Application.Interfaces;
using Microsoft.Extensions.Hosting.Contracts;
using Microsoft.Extensions.Hosting.Domain.Entities;
using Microsoft.Extensions.Hosting.Infrastructure.Persistence;

namespace MassTransitSagaState.Server.Handler;

/// <summary>
/// 进程内消费者
/// </summary>
public class CreateProductHandler(
    AppDbContext db,
    IIntegrationEventPublisher publisher)
    : IConsumer<CreateProductEvent>
{
    public async Task Consume(ConsumeContext<CreateProductEvent> context)
    {
        var product = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = context.Message.Name,
            Description = context.Message.Description,
        };

        db.Products.Add(product);
        await db.SaveChangesAsync(context.CancellationToken);

        await publisher.Publish(new ProductCreated(
            product.Id,
            product.Name,
            product.Description,
            DateTime.UtcNow));
    }
}
