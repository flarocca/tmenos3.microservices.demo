using EasyNetQ;
using EasyNetQ.Consumer;
using Microsoft.Extensions.DependencyInjection;

namespace TMenos3.Microservices.Demo.Microservices.Messaging
{
    public abstract class CommandProcessor : BaseProcessor, ICommandProcessor
    {
        private readonly IBus bus;

        protected CommandProcessor(IBus bus, IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
            this.bus = bus;
        }

        public void Receive() =>
            bus.Receive("stock", AddHandlers);

        protected abstract void AddHandlers(IReceiveRegistration handlers);
    }
}
