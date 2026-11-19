using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Domain.Entities;

namespace Microsoft.Extensions.Hosting.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
}