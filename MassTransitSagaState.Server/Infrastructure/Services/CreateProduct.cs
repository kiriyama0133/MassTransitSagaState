using MassTransit.Mediator;
using MassTransitSagaState.Server.Application.Interfaces;
using Microsoft.Extensions.Hosting.Contracts;

namespace MassTransitSagaState.Server.Infrastructure.Services;

public class CreateProductServive : ICreateProduct
{
    private IScopedMediator _mediator;
    public CreateProductServive(IScopedMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateProductAsync(string name, string description)
    {
        await _mediator.Send(new CreateProductEvent(name, description));
    }
}
