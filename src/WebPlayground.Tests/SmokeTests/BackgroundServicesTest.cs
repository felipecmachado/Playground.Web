using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Threading.Tasks;
using WebPlayground.BackgroundServices;

namespace WebPlayground.Tests
{
    public class BackgroundServicesTest
    {
        private IServiceScopeFactory factory;
        private ILogger<AutomaticInterestService> logger;

        [SetUp]
        public void Setup()
        {
            this.factory = A.Fake<IServiceScopeFactory>();
            this.logger = A.Fake<ILogger<AutomaticInterestService>>();
        }

        [Test]
        public async Task Service_Should_Start_Successfully()
        {
            // Arrange
            var service = new AutomaticInterestService(this.factory, this.logger);

            // Act
            await service.StartAsync(new System.Threading.CancellationToken());

            // Assert
            Assert.IsTrue(service.IsRunning);
        }

        [Test]
        public async Task Service_Should_Stop_Successfully()
        {
            // Arrange
            var service = new AutomaticInterestService(this.factory, this.logger);

            // Act
            await service.StartAsync(new System.Threading.CancellationToken());

            await service.StopAsync(new System.Threading.CancellationToken());

            // Assert
            Assert.IsTrue(!service.IsRunning);
        }
    }
}