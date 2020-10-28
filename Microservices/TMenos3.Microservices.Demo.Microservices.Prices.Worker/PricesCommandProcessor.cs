using EasyNetQ;
using EasyNetQ.Consumer;
using Microsoft.Extensions.DependencyInjection;
using TMenos3.Microservices.Demo.Microservices.Messaging;
using TMenos3.Microservices.Demo.Microservices.Messaging.Commands;

namespace TMenos3.Microservices.Demo.Microservices.Prices.Worker
{
    public class PricesCommandProcessor : CommandProcessor
    {
        public PricesCommandProcessor(IBus bus, IServiceScopeFactory serviceScopeFactory)
            : base(bus, serviceScopeFactory)
        {
        }

        protected override void AddHandlers(IReceiveRegistration handlers) =>
            handlers.Add<CreatePriceForProduct>(DefaultHandleAsync);
    }
}
