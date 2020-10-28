using Microsoft.EntityFrameworkCore;
using TMenos3.Microservices.Demo.Microservices.Prices.Worker.Entities;

namespace TMenos3.Microservices.Demo.Microservices.Prices.Worker.Database
{
    public class PricesDbContext : DbContext
    {
        public DbSet<Price> Prices { get; set; }

        public PricesDbContext(DbContextOptions<PricesDbContext> options)
            : base(options)
        {
        }
    }
}
