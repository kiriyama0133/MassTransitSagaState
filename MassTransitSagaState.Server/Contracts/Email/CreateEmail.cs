namespace MassTransitSagaState.Server.Contracts.Email;

public record CreateEmail(
    Guid ProductId,
    string Name,
    string Description
);
