using MassTransitSagaState.Server.Sagas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MassTransitSagaState.Server.Infrastructure.Persistence.Configurations;

public class NewsletterNotifySagaConfiguration
    : IEntityTypeConfiguration<NewsletterEmailSagaData>
{
    public void Configure(EntityTypeBuilder<NewsletterEmailSagaData> builder)
    {
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
