using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using TMenos3.Microservices.Demo.Microservices.Messaging;
using TMenos3.Microservices.Demo.Microservices.Messaging.Events;

namespace TMenos3.Microservices.Demo.Microservices.Stock.Worker
{
    public class CartEventProcessor : EventProcessor
    {
        public CartEventProcessor(IBus bus, IServiceScopeFactory serviceScopeFactory)
            : base(bus, serviceScopeFactory)
        {
        }

        protected override void AddHandlers(IBus bus)
        {
            bus.SubscribeAsync<PriceForProductCreated>("price", DefaultHandleAsync);
            bus.SubscribeAsync<StockForProductCreated>("stock", DefaultHandleAsync);
        }
    }
}
