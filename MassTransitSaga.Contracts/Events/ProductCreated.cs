namespace MassTransitSaga.Contracts.Events;

/// <summary>
/// 商品已创建，跨进程发布的集成事件。
/// Server 发布 -> Sagas(Worker) 消费。
/// </summary>
public record ProductCreated(
    Guid ProductId,
    string Name,
    string Description,
    DateTime CreatedAt
);
