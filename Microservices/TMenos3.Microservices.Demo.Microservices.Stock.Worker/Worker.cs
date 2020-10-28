using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using TMenos3.Microservices.Demo.Microservices.Messaging;

namespace TMenos3.Microservices.Demo.Microservices.Stock.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ICommandProcessor commandProcessor;

        public Worker(ICommandProcessor commandProcessor) =>
            this.commandProcessor = commandProcessor;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) =>
            commandProcessor.Receive();
    }
}
