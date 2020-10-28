using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Messaging.Commands;
using TMenos3.Microservices.Demo.Microservices.Messaging.Handlers;

namespace TMenos3.Microservices.Demo.Microservices.Prices.Worker
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bus.Receive("price", handlers =>
                handlers.Add<CreatePriceForProduct>(HandleAsync));
        }

        private async Task HandleAsync<T>(T @event)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<T>>();

            await handler.HandleAsync(@event);
        }
    }
}
