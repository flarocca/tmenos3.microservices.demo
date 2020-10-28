using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Messaging.Commands;
using TMenos3.Microservices.Demo.Microservices.Messaging.Events;
using TMenos3.Microservices.Demo.Microservices.Messaging.Handlers;
using TMenos3.Microservices.Demo.Microservices.Prices.Worker.Database;
using TMenos3.Microservices.Demo.Microservices.Prices.Worker.Entities;

namespace TMenos3.Microservices.Demo.Microservices.Prices.Worker.Handlers
{
    public class CreatePriceForProductHandler : IEventHandler<CreatePriceForProduct>
    {
        private readonly PricesDbContext context;
        private readonly IBus bus;

        public CreatePriceForProductHandler(
            PricesDbContext context,
            IBus bus)
        {
            this.context = context;
            this.bus = bus;
        }

        public async Task HandleAsync(CreatePriceForProduct command)
        {
            var price = new Price
            {
                ProductId = command.ProductId,
                Value = command.Price
            };

            context.Prices.Add(price);

            await context.SaveChangesAsync();

            var @event = new PriceForProductCreated
            {
                Id = price.Id,
                ProductId = price.ProductId,
                Price = price.Value
            };

            await bus.PublishAsync(@event, "price");
        }
    }
}
