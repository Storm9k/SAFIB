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
        public IEnumerable<Subvision> subvisions;
        CancellationToken clt; //В данный момент нигде не используется

        public Aservice(IServiceScopeFactory _serviceScopeFactory)
        {
            serviceScopeFactory = _serviceScopeFactory;
            RefreshSubvisions();
            StartAsync(clt);
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

        private void RefreshStatus(object obj)
        {
            Random random = new Random();
            
            foreach (Subvision subvision in subvisions)
            {
                subvision.Status = random.Next(100) <= 50 ? true : false;
            }
        }

        private void RefreshSubvisions()
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                subvisions = dbContext.Subvisions.ToList();
            }
        }

        public void Dispose()
        {
            timer.Dispose();
        }


    }
}
