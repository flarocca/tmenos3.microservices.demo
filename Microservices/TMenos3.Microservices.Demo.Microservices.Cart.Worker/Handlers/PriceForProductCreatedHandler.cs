using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Cart.Worker.Database;
using TMenos3.Microservices.Demo.Microservices.Cart.Worker.Entities;
using TMenos3.Microservices.Demo.Microservices.Messaging.Events;
using TMenos3.Microservices.Demo.Microservices.Messaging.Handlers;

namespace TMenos3.Microservices.Demo.Microservices.Cart.Worker.Handlers
{
    public class PriceForProductCreatedHandler : IEventHandler<PriceForProductCreated>
    {
        private CartDbContext context;

        public PriceForProductCreatedHandler(CartDbContext context) =>
            this.context = context;

        public async Task HandleAsync(PriceForProductCreated @event)
        {
            var price = new ProductPrice
            {
                ExternalId = @event.Id,
                ProductId = @event.ProductId,
                Value = @event.Price
            };

            context.Prices.Add(price);

            await context.SaveChangesAsync();
        }
    }
}
