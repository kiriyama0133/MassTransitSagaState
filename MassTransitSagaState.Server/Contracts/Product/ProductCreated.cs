namespace Microsoft.Extensions.Hosting.Contracts;

public record ProductCreated(
    Guid ProductId,
    string Name,
    string Description,
    DateTime CreatedAt
);