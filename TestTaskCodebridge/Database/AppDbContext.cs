using Microsoft.EntityFrameworkCore;
using TestTaskCodebridge.Domain.Entities;

namespace TestTaskCodebridge.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    
    public DbSet<Dog> Dogs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dog>()
            .HasIndex(d => d.Name)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}