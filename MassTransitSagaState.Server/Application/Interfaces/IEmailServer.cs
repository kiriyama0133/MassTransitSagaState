namespace MassTransitSagaState.Server.Application.Interface;

public interface IEmailServer
{
    Task SendEmailAsync(string name, string description);
}