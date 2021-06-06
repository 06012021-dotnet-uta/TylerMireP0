using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Core.Services;

namespace Core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Creates the host
            var hostBuilder = CreateHostBuilder(args);
            await hostBuilder.RunConsoleAsync();
        }

        //Found this resource to be extremely helpful with this section below https://nbarbettini.gitbooks.io/little-asp-net-core-book/content/chapters/mvc-basics/use-dependency-injection.html
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    //Adds the MediatR service
                    //Have to add AppDomain function to grab Application namespace for MediatR
                    services.AddMediatR(Assembly.GetExecutingAssembly(), AppDomain.CurrentDomain.Load("Application"));

                    //Curtousy of https://andrewlock.net/suppressing-the-startup-and-shutdown-messages-in-asp-net-core/
                    //Just disables console output from host obj --> Also is the only thing that uses dependency injection
                    services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
                    
                    //Adds the store service
                    services.AddHostedService<StoreService>();
                });
        
    }
}
