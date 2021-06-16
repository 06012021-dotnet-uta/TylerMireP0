using ClientApp;
using Microsoft.Extensions.Hosting;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Application;
using System;

namespace Core.Services
{
    //Service to initialize the store object and signal host to end
    public class StoreService : IHostedService
    {
        private Store store;
        private readonly IHostLifetime hostLifetime;
        private readonly DataContext dataContext;

        public StoreService(IHostLifetime hostLifetime, DataContext dataContext)
        {
            this.hostLifetime = hostLifetime;
            this.dataContext = dataContext;
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
            dataContext.Dispose();
            return Task.CompletedTask;
        }
    }
}