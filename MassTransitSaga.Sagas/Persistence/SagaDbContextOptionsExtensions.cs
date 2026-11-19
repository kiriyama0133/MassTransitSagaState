using Microsoft.EntityFrameworkCore;

namespace MassTransitSaga.Sagas.Persistence;

/// <summary>
/// Saga 库的连接配置。运行时（Worker）与设计时（dotnet ef）必须用同一套配置，
/// 否则迁移记录会写进不同的历史表，导致重复建表。
/// </summary>
public static class SagaDbContextOptionsExtensions
{
    /// <summary>
    /// 与业务库共用一个数据库时，必须使用独立的迁移历史表，
    /// 避免和 Server 的 __EFMigrationsHistory 混在一起。
    /// </summary>
    public const string MigrationsHistoryTable = "__SagaMigrationsHistory";

    public static DbContextOptionsBuilder UseSagaPostgres(
        this DbContextOptionsBuilder builder,
        string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Saga store connection string is missing. " +
                "Expected ConnectionStrings:appdb (env: ConnectionStrings__appdb).");
        }

        builder.UseNpgsql(connectionString, npgsql =>
        {
            npgsql.MigrationsHistoryTable(MigrationsHistoryTable);
        });

        return builder;
    }
}
