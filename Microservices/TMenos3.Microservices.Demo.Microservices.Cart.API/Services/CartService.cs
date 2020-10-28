using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Cart.API.Database;
using TMenos3.Microservices.Demo.Microservices.Cart.API.Entities;
using TMenos3.Microservices.Demo.Microservices.Messaging.Commands;

namespace TMenos3.Microservices.Demo.Microservices.Cart.API.Services
{
    public class CartService : ICartService
    {
        private readonly CartDbContext context;
        private readonly IBus bus;

        private static bool IsFeatureEnabled { get; set; } = false;

        public CartService(
            CartDbContext context,
            IBus bus)
        {
            this.context = context;
            this.bus = bus;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await context.Products
                .Include(p => p.Price)
                .Include(p => p.Stock)
                .ToListAsync();

        public async Task<Product> GetAsync(int id) =>
            await context.Products
                .Include(p => p.Price)
                .Include(p => p.Stock)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<int> CreateAsync(string description, int price, int stock)
        {
            var product = new Product
            {
                Description = description
            };

            context.Products.Add(product);

            await context.SaveChangesAsync();

            await CreatePriceAndProduct(product.Id, price, stock);

            return product.Id;
        }

        private async Task CreatePriceAndProduct(int id, int price, int stock)
        {
            if(IsFeatureEnabled == false)
            {
                await bus.SendAsync("price",
                    new CreatePriceForProduct
                    {
                        ProductId = id,
                        Price = price
                    });
            }

            await bus.SendAsync("stock",
                new CreateStockForProduct
                {
                    ProductId = id,
                    Quantity = stock
                });
        }

        public void FeatureToggle() =>
            IsFeatureEnabled = !IsFeatureEnabled;
    }
}
