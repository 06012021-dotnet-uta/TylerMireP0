using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Core.Services;
using MediatR;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            hostBuilder.RunConsoleAsync();
        }

        //Found this resource to be extremely helpful with this section below https://nbarbettini.gitbooks.io/little-asp-net-core-book/content/chapters/mvc-basics/use-dependency-injection.html
        private static IHostBuilder CreateHostBuilder(string[] args) {
            IHostBuilder host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    //Adds the MediatR service and passes in the application namespace assembly to 'mediate'
                    services.AddMediatR(AppDomain.CurrentDomain.Load("Application"));

                    //Curtousy of https://andrewlock.net/suppressing-the-startup-and-shutdown-messages-in-asp-net-core/
                    //Just disables console output from hostBuilder obj
                    services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);

                    //Adds the store service
                    services.AddHostedService<StoreService>();
                });

            return host;
        }

    }
}