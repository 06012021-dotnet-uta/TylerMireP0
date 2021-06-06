using ClientApp;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    public class StoreService : IHostedService
    {
        private Store store;
        public StoreService(IMediator mediator)
        {
            store = new Store(mediator);
        }

        public Task StartAsync(CancellationToken ct)
        {
            store.Open();
            return Task.CompletedTask;  
        }

        public Task StopAsync(CancellationToken ct)
        {
            Console.WriteLine("Service is ending!");
            return Task.CompletedTask;   
        }
    }
}