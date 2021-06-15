
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Core.Services;
using Persistence;
using Microsoft.Extensions.Logging;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //Runs the host
            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) {
            bool debug = false;
            IHostBuilder host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    //Adds the store service
                    services.AddHostedService<StoreService>();

                    //Adds the db context service
                    services.AddDbContext<DataContext>();

                    //Curtousy of https://andrewlock.net/suppressing-the-startup-and-shutdown-messages-in-asp-net-core/
                    //Just disables console output from hostBuilder obj
                    services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
                })
                .ConfigureLogging(logging =>
                {
                    if(!debug){
                    //Disbale ef core sql outputs
                    logging
                        .SetMinimumLevel(LogLevel.Warning);
                    }
                });

            return host;
        }

    }
}