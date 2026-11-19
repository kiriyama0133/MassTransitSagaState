using Microsoft.EntityFrameworkCore;

namespace MassTransitSaga.Sagas.Persistence;

/// <summary>
/// Saga 专属的 DbContext：只映射 saga 状态表，不碰业务表。
/// 这是以后把 saga 拆成独立服务/独立数据库的前提。
/// </summary>
public class SagaDbContext : DbContext
{
    public SagaDbContext(DbContextOptions<SagaDbContext> options) : base(options)
    {
    }

    public DbSet<NewsletterEmailSagaData> NewsletterEmailSagaDatas => Set<NewsletterEmailSagaData>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SagaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
