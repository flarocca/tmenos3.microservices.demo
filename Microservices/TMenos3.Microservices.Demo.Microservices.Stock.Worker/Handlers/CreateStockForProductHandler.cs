using EasyNetQ;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Messaging.Commands;
using TMenos3.Microservices.Demo.Microservices.Messaging.Events;
using TMenos3.Microservices.Demo.Microservices.Messaging.Handlers;
using TMenos3.Microservices.Demo.Microservices.Stock.Worker.Database;

namespace TMenos3.Microservices.Demo.Microservices.Stock.Worker.Handlers
{
    public class CreateStockForProductHandler : IEventHandler<CreateStockForProduct>
    {
        private readonly StockDbContext context;
        private readonly IBus bus;

        public CreateStockForProductHandler(
            StockDbContext context,
            IBus bus)
        {
            this.context = context;
            this.bus = bus;
        }

        public async Task HandleAsync(CreateStockForProduct command)
        {
            var stock = new Entities.Stock
            {
                ProductId = command.ProductId,
                Quantity = command.Quantity
            };

            context.Stocks.Add(stock);

            await context.SaveChangesAsync();

            var @event = new StockForProductCreated
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity
            };

            await bus.PublishAsync(@event, "price");
        }
    }
}
