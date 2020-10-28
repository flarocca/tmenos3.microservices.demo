using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Cart.Worker.Database;
using TMenos3.Microservices.Demo.Microservices.Cart.Worker.Entities;
using TMenos3.Microservices.Demo.Microservices.Messaging.Events;
using TMenos3.Microservices.Demo.Microservices.Messaging.Handlers;

namespace TMenos3.Microservices.Demo.Microservices.Cart.Worker.Handlers
{
    public class StockForProductCreatedHandler : IEventHandler<StockForProductCreated>
    {
        private readonly CartDbContext context;

        public StockForProductCreatedHandler(CartDbContext context) =>
            this.context = context;

        public async Task HandleAsync(StockForProductCreated @event)
        {
            var price = new ProductStock
            {
                ExternalId = @event.Id,
                ProductId = @event.ProductId,
                Quantity = @event.Quantity
            };

            context.Stocks.Add(price);

            await context.SaveChangesAsync();
        }
    }
}
