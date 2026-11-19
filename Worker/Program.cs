using MassTransit;
using MassTransitSaga.Sagas;
using MassTransitSaga.Sagas.Persistence;
using MassTransitSagaState.EmailPage.Worker.Handlers;
using MassTransitSagaState.EmailPage.Worker.Services;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

var rabbitMqConnectionString = builder.Configuration.GetConnectionString("messaging");
var sagaStoreConnectionString = builder.Configuration.GetConnectionString("appdb");

if (string.IsNullOrWhiteSpace(rabbitMqConnectionString))
{
    throw new InvalidOperationException(
        "RabbitMQ connection string 'messaging' is missing. " +
        "Expected env: ConnectionStrings__messaging");
}

if (string.IsNullOrWhiteSpace(sagaStoreConnectionString))
{
    throw new InvalidOperationException(
        "Saga store connection string 'appdb' is missing. " +
        "Expected env: ConnectionStrings__appdb");
}

builder.Services.AddNewsletterSagaStore(sagaStoreConnectionString);
builder.Services.AddScoped<IEmailServer, SmtpEmailSender>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendEmailHandler>();
    x.AddConsumer<EmailSentIntegrationEventConsumer>();

    // 跨服务的编排者
    x.AddNewsletterSaga();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(rabbitMqConnectionString));
        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();

await using (var scope = host.Services.CreateAsyncScope())
{
    var sagaDbContext = scope.ServiceProvider.GetRequiredService<SagaDbContext>();
    await sagaDbContext.Database.MigrateAsync();
}

host.Run();
