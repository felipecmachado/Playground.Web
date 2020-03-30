using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebPlayground.BackgroundServices;

namespace WebPlayground.Tests
{
    public class BackgroundServicesTest
    {
        private IServiceScopeFactory factory;
        private ILogger<AutomaticInterestService> logger;

        [TestInitialize]
        public void Setup()
        {
            this.factory = A.Fake<IServiceScopeFactory>();
            this.logger = A.Fake<ILogger<AutomaticInterestService>>();
        }

        [TestMethod]
        public async Task Service_Should_Start_Successfully()
        {
            // Arrange
            var service = new AutomaticInterestService(this.factory, this.logger);

            // Act
            await service.StartAsync(new System.Threading.CancellationToken());

            // Assert
            Assert.IsTrue(service.IsRunning);
        }

        [TestMethod]
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