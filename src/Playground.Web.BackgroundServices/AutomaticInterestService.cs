using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Playground.Web.Business.Interfaces;
using Playground.Web.Infrastructure;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reactive.Disposables;
using System.Reactive.Concurrency;

namespace Playground.Web.BackgroundServices
{
    /// <summary>
    /// Background service responsible for paying interest to the checking accounts
    /// <seealso cref="https://www.bankrate.com/glossary/i/interest-checking-account/"/>
    /// </summary>
    public class AutomaticInterestService : IHostedService, IDisposable
    {
        public const string INTERESTRATE_KEY = "AnnualInterestRate";

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AutomaticInterestService> _logger;
        
        private CompositeDisposable disposables;
        public bool IsRunning { get; private set; }

        public AutomaticInterestService(IServiceScopeFactory factory, ILogger<AutomaticInterestService> logger)
        {
            this._scopeFactory = factory;
            this._logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"AutomaticInterestService is starting...");

            this.disposables?.Dispose();

            this.disposables = new CompositeDisposable
            {
                this.RunAutomaticInterestRate(),
                this.RunHeartbeat()
            };

            this.IsRunning = true;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"AutomaticInterestService is stopping...");

            this.disposables.Dispose();

            this.IsRunning = false;

            return Task.CompletedTask;
        }

        private IDisposable RunAutomaticInterestRate()
        {
            var scheduler = TaskPoolScheduler.Default.ScheduleAsync(async (s, ct) =>
            {
                while (!ct.IsCancellationRequested)
                {
                    var interval = (DateTime.Today.AddDays(1) - DateTime.Now);

                    Thread.Sleep(interval);

                    _logger.LogInformation($"Taking a screenshot of all accounts...");

                    await SaveBalances();

                    _logger.LogInformation($"Applying automatic interest to all accounts...");

                    await ApplyInterestRate();
                }
            });

            return scheduler;
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

        private async Task ApplyInterestRate()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BankContext>();

                var accounts = dbContext.CheckingAccounts.Where(x => x.AutomaticInterest).ToListAsync();
                var rate = dbContext.Settings
                    .Where(x => x.Key == INTERESTRATE_KEY)
                    .Select(x => Decimal.Parse(x.Value) / 365) // Get daily rate
                    .FirstOrDefaultAsync();

                foreach (var account in await accounts)
                {
                    //TODO:
                }
            }
        }

        private async Task SaveBalances()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BankContext>();
                var managementService = scope.ServiceProvider.GetRequiredService<IAccountManagementService>();

                var accounts = dbContext.CheckingAccounts.ToListAsync();

                foreach (var account in await accounts)
                {
                    await managementService.SaveBalance(account.CheckingAccountId);
                }
            }
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
