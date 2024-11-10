using Microsoft.EntityFrameworkCore;
using DogsHouseService.Models;

namespace DogsHouseService.Data
{
    public class DogsHouseContext : DbContext
    {
        public DogsHouseContext(DbContextOptions<DogsHouseContext> options) : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; }

        // To ensure that the database is created and seed data is present
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>()
                .HasIndex(d => d.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}