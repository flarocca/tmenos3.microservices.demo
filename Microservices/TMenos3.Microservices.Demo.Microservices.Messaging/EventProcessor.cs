using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace TMenos3.Microservices.Demo.Microservices.Messaging
{
    public abstract class EventProcessor : BaseProcessor, IEventProcessor
    {
        private readonly IBus bus;

        protected EventProcessor(IBus bus, IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
            this.bus = bus;
        }

        public void Subscribe() => AddHandlers(bus);

        protected abstract void AddHandlers(IBus bus);
    }
}
