using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using SAFIB.Models;
using Microsoft.Extensions.DependencyInjection;

namespace SAFIB.Services
{
    public class Aservice : IHostedService, IDisposable
    {
        private Timer timer;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public Aservice()
        {

        }

        public Aservice(IServiceScopeFactory _serviceScopeFactory)
        {
            serviceScopeFactory = _serviceScopeFactory;
        }

        public Task StartAsync (CancellationToken cancellationToken)
        {
            timer = new Timer(RefreshStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(3.0));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void RefreshStatus(object state)
        {
            Random random = new Random();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var bservise = scope.ServiceProvider.GetRequiredService<Bservice>();
                foreach (Subvision subvision in bservise.SubvisionsResult)
                {
                    subvision.Status = random.Next(100) <= 50 ? true : false;
                }
            }

        }

        public void Dispose()
        {
            timer.Dispose();
        }


    }
}
