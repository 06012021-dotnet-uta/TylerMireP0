using ClientApp;
using MediatR;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    public class StoreService : IHostedService
    {
        private Store store;
        private readonly IHostLifetime hostLifetime;

        public StoreService(IMediator mediator, IHostLifetime hostLifetime)
        {
            this.hostLifetime = hostLifetime;
            store = new Store(mediator);
        }

        public Task StartAsync(CancellationToken ct)
        {
            store.Run();
            return hostLifetime.StopAsync(ct);
        }

        public Task StopAsync(CancellationToken ct)
        {
            //Put a save changes to db function here maybe? Dunno if it would be async
            return Task.CompletedTask; //Just to make StopAsync happy
        }
    }
}