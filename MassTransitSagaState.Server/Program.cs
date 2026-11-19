
using MassTransitSagaState.Server.Application.Interfaces;
using MassTransitSagaState.Server.Extension;
using MassTransitSagaState.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
// services
builder.Services.AddServices();
// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddRedisClientBuilder("cache")
    .WithOutputCache();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// pg
builder.Services.AddAppDatabase(builder.Configuration);
// masstransit
builder.Services.AddMassTransitService(builder.Configuration);
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// migration
using (var scope = app.Services.CreateScope())
{
    var dbContet = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();
    await dbContet.Database.MigrateAsync();
}

app.UseOutputCache();

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

var api = app.MapGroup("/api");
api.MapGet("weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(5)))
.WithName("GetWeatherForecast");

api.MapGet("product", async ([FromServices] ICreateProduct productService) =>
{
    await productService.CreateProductAsync("kiriyama", "hello kiriyama!");
    return Results.Ok();
});
app.MapDefaultEndpoints();

app.UseFileServer();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
