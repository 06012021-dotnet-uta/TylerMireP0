using ClientApp;
using Microsoft.Extensions.Hosting;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Application;

namespace Core.Services
{
    //Service to initialize the store object and signal host to end
    public class StoreService : IHostedService
    {
        private Store store;
        private readonly IHostLifetime hostLifetime;

        public StoreService(IHostLifetime hostLifetime, DataContext dataContext)
        {
            this.hostLifetime = hostLifetime;
            BusinessApplicaiton businessApplication = new BusinessApplicaiton(dataContext);
            store = new Store(businessApplication);
        }

        public Task StartAsync(CancellationToken ct)
        {
            store.Run();
            return hostLifetime.StopAsync(ct);
        }

        public Task StopAsync(CancellationToken ct)
        {
            //Put a save changes to db function here maybe? Dunno if it would be async
            return Task.CompletedTask;
        }
    }
}