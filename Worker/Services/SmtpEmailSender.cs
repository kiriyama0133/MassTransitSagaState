namespace MassTransitSagaState.EmailPage.Worker.Services;

public class SmtpEmailSender(ILogger<SmtpEmailSender> logger) : IEmailServer
{
    public async Task SendEmailAsync(string name, string description)
    {
        // 占位实现，真实场景替换为 MailKit / SMTP 客户端
        logger.LogInformation("Sending email to {Name}: {Description}", name, description);
        await Task.CompletedTask;
    }
}
