using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MassTransitSaga.Sagas.Persistence;

/// <summary>
/// 供 dotnet ef 使用的设计时工厂。
/// 只读环境变量，保证和 Worker 运行时拿到的连接串完全一致。
/// </summary>
public class SagaDbContextFactory : IDesignTimeDbContextFactory<SagaDbContext>
{
    public SagaDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__appdb");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "ConnectionStrings__appdb is not configured. " +
                "Set it before running dotnet ef, e.g. " +
                "ConnectionStrings__appdb='Host=localhost;Port=5432;Database=appdb;Username=postgres;Password=***'");
        }

        var options = new DbContextOptionsBuilder<SagaDbContext>();
        options.UseSagaPostgres(connectionString);

        return new SagaDbContext(options.Options);
    }
}
