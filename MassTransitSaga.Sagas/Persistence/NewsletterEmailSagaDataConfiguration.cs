using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MassTransitSaga.Sagas.Persistence;

public class NewsletterEmailSagaDataConfiguration
    : IEntityTypeConfiguration<NewsletterEmailSagaData>
{
    public void Configure(EntityTypeBuilder<NewsletterEmailSagaData> builder)
    {
        // 复用 appdb 里既有的表（由 Server 的历史迁移创建），列定义必须保持一致。
        builder.ToTable("NewsletterEmailSagaDatas");

        builder.HasKey(x => x.CorrelationId);
        builder.Property(x => x.CurrentState)
            .HasMaxLength(64)
            .IsRequired();
        builder.Property(x => x.Name)
            .HasMaxLength(256);
        builder.Property(x => x.Description)
            .HasMaxLength(2000);
    }
}
