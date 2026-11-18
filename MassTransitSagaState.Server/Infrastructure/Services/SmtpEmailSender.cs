using MassTransitSagaState.Server.Application.Interface;

namespace MassTransitSagaState.Server.Infrastructure.Services;

public class SmtpEmailSender : IEmailServer
{
    public async Task SendEmailAsync(string name, string description)
    {
        await Console.Out.WriteLineAsync($"Sending email to {name}");
    }
}