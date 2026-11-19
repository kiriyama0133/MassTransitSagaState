namespace MassTransitSaga.Contracts.Commands;

/// <summary>
/// 发送邮件的命令。
/// Sagas(Worker) 发布 -> Worker 的发信消费者执行。
/// </summary>
public record CreateEmail(
    Guid ProductId,
    string Name,
    string Description
);
