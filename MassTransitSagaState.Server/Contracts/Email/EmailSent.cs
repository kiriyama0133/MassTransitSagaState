using MassTransit.Futures.Contracts;

namespace MassTransitSagaState.Server.Contracts.Email;

public record EmailSent(
    Guid ProductId,
    string Name,
    string Description
);
