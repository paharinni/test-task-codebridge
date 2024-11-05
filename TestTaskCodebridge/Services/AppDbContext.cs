using Microsoft.EntityFrameworkCore;
using TestTaskCodebridge.Domain.Entities;

namespace TestTaskCodebridge.Services;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Dog> Dogs { get; set; }
}