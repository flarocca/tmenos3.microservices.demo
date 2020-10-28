using System.Threading.Tasks;

namespace TMenos3.Microservices.Demo.Microservices.Messaging.Handlers
{
    public interface IEventHandler<T>
    {
        Task HandleAsync(T @event);
    }
}
