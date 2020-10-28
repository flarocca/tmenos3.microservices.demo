using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Monolith.Database;
using TMenos3.Microservices.Demo.Monolith.Database.Entities;

namespace TMenos3.Microservices.Demo.Monolith.Stock
{
    public class StockService : IStockService
    {
        private readonly MonolithDbContext context;

        public StockService(MonolithDbContext context)
        {
            this.context = context;
        }

        public async Task<ProductStock> AddAsync(int productId, int quantity)
        {
            var stock = new ProductStock
            {
                ProductId = productId,
                Quantity = quantity
            };

            context.Stocks.Add(stock);

            await context.SaveChangesAsync();

            return stock;
        }

        public async Task<ProductStock> GetAsync(int id)
        {
            var stock = await context.Stocks
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            return stock;
        }

        public async Task<ProductStock> GetByProductAsync(int productId)
        {
            var stock = await context.Stocks
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.ProductId == productId);

            return stock;
        }
    }
}
