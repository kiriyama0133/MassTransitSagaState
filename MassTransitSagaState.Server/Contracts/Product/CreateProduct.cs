namespace Microsoft.Extensions.Hosting.Contracts;

public record CreateProductEvent(
    string Name,
    string Description
);