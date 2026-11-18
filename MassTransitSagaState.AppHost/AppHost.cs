var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pg");
var appDb = postgres.AddDatabase("appdb");
var cache = builder.AddRedis("cache");

var server = builder.AddProject<Projects.MassTransitSagaState_Server>("server")
    .WithReference(cache)
    .WaitFor(cache)
    .WithHttpHealthCheck("/health")
    .WithReference(appDb)
    .WaitFor(appDb)
    .WithExternalHttpEndpoints();

var webfrontend = builder.AddViteApp("webfrontend", "../frontend")
    .WithReference(server)
    .WaitFor(server);

server.PublishWithContainerFiles(webfrontend, "wwwroot");

builder.Build().Run();
