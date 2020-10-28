using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMenos3.Microservices.Demo.Microservices.Cart.Worker.Database;
using TMenos3.Microservices.Demo.Microservices.Cart.Worker.Handlers;
using TMenos3.Microservices.Demo.Microservices.Messaging;
using TMenos3.Microservices.Demo.Microservices.Messaging.Events;
using TMenos3.Microservices.Demo.Microservices.Messaging.Handlers;
using TMenos3.Microservices.Demo.Microservices.Stock.Worker;

namespace TMenos3.Microservices.Demo.Microservices.Cart.Worker
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args)
                .Build()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    builder.AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    var optionsBuilder = new DbContextOptionsBuilder<CartDbContext>();
                    optionsBuilder.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));

                    services.AddScoped(sp => new CartDbContext(optionsBuilder.Options));

                    services.AddScoped<IEventHandler<PriceForProductCreated>, PriceForProductCreatedHandler>();

                    services.AddScoped<IEventHandler<StockForProductCreated>, StockForProductCreatedHandler>();

                    services.AddScoped<IEventProcessor, CartEventProcessor>();

                    services.AddSingleton(sp => RabbitHutch.CreateBus("host=localhost"));
                });
    }
}
