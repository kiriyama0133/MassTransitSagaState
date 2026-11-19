namespace MassTransitSagaState.EmailPage.Worker.Services;

public interface IEmailServer
{
    Task SendEmailAsync(string name, string description);
}
