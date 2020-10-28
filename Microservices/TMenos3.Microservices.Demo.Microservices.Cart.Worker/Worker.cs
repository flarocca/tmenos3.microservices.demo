using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Messaging.Events;
using TMenos3.Microservices.Demo.Microservices.Messaging.Handlers;

namespace TMenos3.Microservices.Demo.Microservices.Cart.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IBus bus;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public Worker(IBus bus, IServiceScopeFactory serviceScopeFactory)
        {
            this.bus = bus;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bus.SubscribeAsync<PriceForProductCreated>("price", HandleAsync);
            bus.SubscribeAsync<StockForProductCreated>("stock", HandleAsync);

            return Task.CompletedTask;
        }

        private async Task HandleAsync<T>(T @event)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<T>>();

            await handler.HandleAsync(@event);
        }
    }
}
