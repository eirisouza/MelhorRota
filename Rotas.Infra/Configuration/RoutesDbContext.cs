using Microsoft.EntityFrameworkCore;
using Routes.Domain.Entities;

namespace Routes.Infra.Configuration;

public class RoutesDbContext(DbContextOptions<RoutesDbContext> options) : DbContext(options)
{
    public DbSet<Route> Routes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Route>()
            .Property(p => p.Source)
            .HasMaxLength(3)
            .IsRequired();

        modelBuilder.Entity<Route>()
            .Property(p => p.Target)            
            .HasMaxLength(3)
            .IsRequired();

        modelBuilder.Entity<Route>()
            .Property(p => p.Value)
            .HasMaxLength(3)
            .IsRequired();

        modelBuilder.Entity<Route>()
            .HasKey(k => new { k.Source, k.Target });

        base.OnModelCreating(modelBuilder);
    }

}
