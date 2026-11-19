using MassTransit.Mediator;

namespace MassTransitSagaState.Server.Application.Interfaces;

public interface ICreateProduct
{
    public Task CreateProductAsync(string name, string description);
}
