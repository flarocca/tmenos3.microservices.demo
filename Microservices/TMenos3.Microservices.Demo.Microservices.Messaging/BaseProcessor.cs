using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Messaging.Handlers;

namespace TMenos3.Microservices.Demo.Microservices.Messaging
{
    public abstract class BaseProcessor
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        protected BaseProcessor(IServiceScopeFactory serviceScopeFactory) =>
            this.serviceScopeFactory = serviceScopeFactory;

        protected async Task DefaultHandleAsync<T>(T @event)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<T>>();

            await handler.HandleAsync(@event);
        }
    }
}
