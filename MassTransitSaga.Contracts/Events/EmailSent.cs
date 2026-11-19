namespace MassTransitSaga.Contracts.Events;

/// <summary>
/// 邮件已发送，回传给 Saga 推进状态。
/// </summary>
public record EmailSent(
    Guid ProductId,
    string Name,
    string Description
);
