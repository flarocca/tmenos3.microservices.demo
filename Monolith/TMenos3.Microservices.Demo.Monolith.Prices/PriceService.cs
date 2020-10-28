using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Monolith.Database;
using TMenos3.Microservices.Demo.Monolith.Database.Entities;

namespace TMenos3.Microservices.Demo.Monolith.Prices
{
    public class PriceService : IPriceService
    {
        private readonly MonolithDbContext context;

        public PriceService(MonolithDbContext context)
        {
            this.context = context;
        }

        public async Task<ProductPrice> AddAsync(int productId, int value)
        {
            var price = new ProductPrice
            {
                ProductId = productId,
                Value = value
            };

            context.Prices.Add(price);

            await context.SaveChangesAsync();

            return price;
        }

        public async Task<ProductPrice> GetAsync(int id)
        {
            var price = await context.Prices
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            return price;
        }

        public async Task<ProductPrice> GetByProductAsync(int productId)
        {
            var price = await context.Prices
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.ProductId == productId);

            return price;
        }
    }
}
