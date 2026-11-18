using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Infrastructure.Persistence;
using Npgsql;

namespace MassTransitSagaState.Server.Extension;

public static class DbExtension
{
    public static IServiceCollection AddAppDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("appdb")
                               ?? throw new InvalidOperationException("Connection string 'appdb' not found.");

        var dataSource = new NpgsqlDataSourceBuilder(connectionString).Build();
        services.AddSingleton<NpgsqlDataSource>(dataSource);
        services.AddDbContextPool<AppDbContext>(opt =>
        {
            opt.UseNpgsql(dataSource);
        });
        return services;
    }
}