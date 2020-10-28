using Microsoft.EntityFrameworkCore;

namespace TMenos3.Microservices.Demo.Microservices.Stock.Worker.Database
{
    public class StockDbContext : DbContext
    {
        public DbSet<Entities.Stock> Stocks { get; set; }

        public StockDbContext(DbContextOptions<StockDbContext> options)
            : base(options)
        {
        }
    }
}
