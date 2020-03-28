using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebPlayground.BackgroundServices
{
    /// <summary>
    /// Background service responsible for paying interest to the checking accounts
    /// <seealso cref="https://www.bankrate.com/glossary/i/interest-checking-account/"/>
    /// </summary>
    public class AutomaticInterestService : IHostedService
    {
        // TODO: get this from settings
        public const float DAILY_RATE = (0.025f / 365); // 2.25% annual

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // discover daily rate
            // save daily balances
            // apply interests
            // finish
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
