using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Web.BackgroundServices
{
    /// <summary>
    /// Background service responsible for transfering funds between checking accounts
    /// </summary>
    public class TransferManagementService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AutomaticInterestService> _logger;
        
        private CompositeDisposable disposables;
        public bool IsRunning { get; private set; }

        public TransferManagementService(IServiceScopeFactory factory, ILogger<AutomaticInterestService> logger)
        {
            this._scopeFactory = factory;
            this._logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"TransferManagementService is starting...");

            this.disposables?.Dispose();

            this.disposables = new CompositeDisposable
            {
                this.RunExecuteTransfers(),
                this.RunHeartbeat()
            };

            this.IsRunning = true;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"TransferManagementService is stopping...");

            this.disposables.Dispose();

            this.IsRunning = false;

            return Task.CompletedTask;
        }

        private IDisposable RunExecuteTransfers()
        {
            var scheduler = TaskPoolScheduler.Default.ScheduleAsync(async (s, ct) =>
            {
                while (!ct.IsCancellationRequested)
                {
                    Thread.Sleep(TimeSpan.FromMinutes(30));

                    _logger.LogInformation($"Executing transfers...");

                    await ExecuteTransfers();
                }
            });

            return scheduler;
        }

        private Task ExecuteTransfers()
        {
            return null;
        }

        private IDisposable RunHeartbeat()
        {
            var kpiTimer = new Timer(o =>
            {
                try
                {
                    // TODO: Send heartbeat using SignalR
                    _logger.LogDebug($"Sending heartbeat...");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while making an HTTP request. Check if the API is online.");
                }
            }, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));

            return kpiTimer;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                disposables?.Dispose();
            }
        }
    }
}
