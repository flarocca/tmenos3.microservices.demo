using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Monolith.Database;
using TMenos3.Microservices.Demo.Monolith.Database.Entities;
using TMenos3.Microservices.Demo.Monolith.Prices;
using TMenos3.Microservices.Demo.Monolith.Stock;

namespace TMenos3.Microservices.Demo.Monolith.Cart
{
    public class CartService : ICartService
    {
        private readonly MonolithDbContext context;
        private readonly IPriceService priceService;
        private readonly IStockService stockService;

        private static Action ErrorFeature = () => throw new NotImplementedException();
        private static Action EmptyFeature = () => { };

        public CartService(
            MonolithDbContext context,
            IPriceService priceService,
            IStockService stockService)
        {
            this.context = context;
            this.priceService = priceService;
            this.stockService = stockService;
        }

        private static bool IsFeatureEnabled { get; set; } = false;

        private static Action NewFeature { get; set; } = EmptyFeature;

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            NewFeature();

            var products = await context.Products
                .ToListAsync();

            foreach (var product in products)
            {
                var price = await priceService.GetByProductAsync(product.Id);
                var stock = await stockService.GetByProductAsync(product.Id);

                product.Price = price;
                product.Stock = stock;
            }

            return products;
        }

        public async Task<Product> GetAsync(int id)
        {
            NewFeature();

            var product = await context.Products
                .FirstOrDefaultAsync(s => s.Id == id);

            var price = await priceService.GetByProductAsync(id);
            var stock = await stockService.GetByProductAsync(id);

            product.Price = price;
            product.Stock = stock;

            return product;
        }

        public async Task CreateAsync(string description, int price, int stock)
        {
            NewFeature();

            var product = new Product
            {
                Description = description,
                Price = new ProductPrice
                {
                    Value = price
                },
                Stock = new ProductStock
                {
                    Quantity = stock
                }
            };

            context.Products.Add(product);

            await context.SaveChangesAsync();
        }

        public void FeatureToggle()
        {
            IsFeatureEnabled = !IsFeatureEnabled;

            NewFeature = IsFeatureEnabled ? ErrorFeature : EmptyFeature;
        }
    }
}
