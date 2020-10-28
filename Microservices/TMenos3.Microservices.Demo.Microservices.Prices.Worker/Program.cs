using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMenos3.Microservices.Demo.Microservices.Messaging;
using TMenos3.Microservices.Demo.Microservices.Messaging.Commands;
using TMenos3.Microservices.Demo.Microservices.Messaging.Handlers;
using TMenos3.Microservices.Demo.Microservices.Prices.Worker.Database;
using TMenos3.Microservices.Demo.Microservices.Prices.Worker.Handlers;

namespace TMenos3.Microservices.Demo.Microservices.Prices.Worker
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

                    var optionsBuilder = new DbContextOptionsBuilder<PricesDbContext>();
                    optionsBuilder.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));

                    services.AddScoped(sp => new PricesDbContext(optionsBuilder.Options));

                    services.AddScoped<IEventHandler<CreatePriceForProduct>, CreatePriceForProductHandler>();

                    services.AddScoped<ICommandProcessor, PricesCommandProcessor>();

                    services.AddSingleton(sp => RabbitHutch.CreateBus("host=localhost"));
                });
    }
}
