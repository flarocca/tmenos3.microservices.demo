using Microsoft.EntityFrameworkCore;
using TMenos3.Microservices.Demo.Microservices.Cart.Worker.Entities;

namespace TMenos3.Microservices.Demo.Microservices.Cart.Worker.Database
{
    public class CartDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPrice> Prices { get; set; }

        public DbSet<ProductStock> Stocks { get; set; }

        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options)
        {
        }
    }
}
