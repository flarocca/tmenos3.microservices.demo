using Microsoft.EntityFrameworkCore;
using TMenos3.Microservices.Demo.Monolith.Database.Entities;

namespace TMenos3.Microservices.Demo.Monolith.Database
{
    public class MonolithDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPrice> Prices { get; set; }

        public DbSet<ProductStock> Stocks { get; set; }

        public MonolithDbContext(DbContextOptions<MonolithDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
