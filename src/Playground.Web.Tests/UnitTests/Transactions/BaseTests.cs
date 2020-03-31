using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Playground.Web.Business.Interfaces;
using Playground.Web.Business.Services;
using Playground.Web.Infrastructure;
using Playground.Web.Shared.Common;

namespace Playground.Web.Tests.UnitTests
{
    public class BaseTests
    {
        public ServiceProvider _serviceProvider;

        public readonly ITransactionService _operationsService;
        public readonly ICheckingAccountService _myCheckingAccountService;
        public readonly IAccountManagementService _accountManagementService;
        public readonly IUserService _userService;

        public BaseTests()
        {
            var services = new ServiceCollection();

            services.AddDbContext<BankContext>
                (options => options
                    .UseInMemoryDatabase("web-playground")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                );

            services.AddTransient<IAccountManagementService, AccountManagementService>();
            services.AddTransient<ICheckingAccountService, CheckingAccountService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IUserService, UserService>();

            services.AddSingleton<AppSettings>(x => { return new AppSettings() { Secret = string.Empty }; });

            this._serviceProvider = services.BuildServiceProvider();

            this._operationsService = _serviceProvider.GetRequiredService<ITransactionService>();
            this._myCheckingAccountService = _serviceProvider.GetRequiredService<ICheckingAccountService>();
            this._accountManagementService = _serviceProvider.GetRequiredService<IAccountManagementService>();
            this._userService = _serviceProvider.GetRequiredService<IUserService>();
        }

        [SetUp]
        public void SetUp()
        {
            var context = this._serviceProvider.GetService<BankContext>();

            DbInitializer.SeedSettings(context);
            DbInitializer.SeedUsers(context);
            DbInitializer.SeedBranches(context);
            DbInitializer.SeedAccounts(context);
        }
    }
}