using MassTransit;
using Microsoft.Extensions.Hosting.Contracts;
using Microsoft.Extensions.Hosting.Domain.Entities;
using Microsoft.Extensions.Hosting.Infrastructure.Persistence;

namespace MassTransitSagaState.Server.Handler;

public class CreateProductHandler(AppDbContext dbContext) 
    : IConsumer<CreateProduct>
{
    public async Task Consume(ConsumeContext<CreateProduct> context)
    {
        var product = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = context.Message.Name,
            Description =  context.Message.Description,
        };

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        
        await context.Publish(new ProductCreated(
            product.Id,
            product.Name,
            "kiriyama",
            DateTime.UtcNow
        ));
    }
}