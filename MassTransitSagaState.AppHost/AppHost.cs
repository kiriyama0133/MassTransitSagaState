var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pg");
var appDb = postgres.AddDatabase("appdb");
var cache = builder.AddRedis("cache");
var rabbitmq = builder.AddRabbitMQ("messaging")
        .WithManagementPlugin();

var server = builder.AddProject<Projects.MassTransitSagaState_Server>("server")
    .WithReference(cache)
    .WaitFor(cache)
    .WithHttpHealthCheck("/health")
    .WithReference(appDb)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WaitFor(appDb)
    .WithExternalHttpEndpoints();

var webfrontend = builder.AddViteApp("webfrontend", "../frontend")
    .WithReference(server)
    .WaitFor(server);

server.PublishWithContainerFiles(webfrontend, "wwwroot");

builder.AddProject<Projects.Worker>("worker")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    // saga 状态表放在 appdb 里，Worker 需要自己的 DbContext 连接
    .WithReference(appDb)
    .WaitFor(appDb);

builder.Build().Run();
