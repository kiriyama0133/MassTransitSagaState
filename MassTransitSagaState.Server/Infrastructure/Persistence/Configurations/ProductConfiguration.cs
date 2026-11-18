using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting.Domain.Entities;

namespace Microsoft.Extensions.Hosting.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("ProductId")
            .ValueGeneratedOnAdd();
        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.Description)
            .HasColumnName("Description")
            .HasMaxLength(500)
            .IsRequired();
    }
}