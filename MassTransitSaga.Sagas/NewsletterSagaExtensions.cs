using MassTransit;
using MassTransitSaga.Sagas.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransitSaga.Sagas;

public static class NewsletterSagaExtensions
{
    public static IServiceCollection AddNewsletterSagaStore(
        this IServiceCollection services,
        string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Saga store connection string is missing. " +
                "Expected ConnectionStrings:appdb (env: ConnectionStrings__appdb).");
        }

        services.AddDbContext<SagaDbContext>(options => options.UseSagaPostgres(connectionString));

        return services;
    }

    /// <summary>
    /// 队列"newsletter-notify-saga"。
    /// </summary>
    public static void AddNewsletterSaga(this IBusRegistrationConfigurator configurator)
    {
        configurator.AddSagaStateMachine<NewsletterNotifySaga, NewsletterEmailSagaData>()
            .Endpoint(e => e.Name = "newsletter-notify-saga")
            .EntityFrameworkRepository(repository =>
            {
                repository.ExistingDbContext<SagaDbContext>();
                repository.UsePostgres();
            });
    }
}
