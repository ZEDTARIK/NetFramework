using ApiMinimalEntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiMinimalEntityFramework.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options): base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Genre>().Property(g => g.Name).IsRequired().HasMaxLength(50);
        }
        public DbSet<Genre> Genres { get; set; }

    }
}
