using NUnit.Framework;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Shared.Requests;
using System.Threading.Tasks;

namespace Playground.Web.Tests.UnitTests
{
    public class DepositTests : BaseTests
    {
        private const int USER_ID = 1;
        private const int ACCOUNT_ID = 1;

        private CheckingAccount checkingAccount;

        public DepositTests()
        {
        }

        [SetUp]
        public async Task Setup()
        {
            this.checkingAccount = (await this._myCheckingAccountService.GetCheckingAccount(USER_ID, ACCOUNT_ID));
        }

        [Test]
        public async Task DepositMustBeSuccessful() 
        {
            // Arrange
            var request = new DepositRequest()
            { 
                AccountNumber = this.checkingAccount.AccountNumber,
                Amount = 50,
                Token = this.checkingAccount.Token
            };

            // Act
            var previousValue = await this._myCheckingAccountService.GetBalance(1, 1);

            var transaction = await _operationsService.Deposit(1, request);

            if (transaction.Code != Responses.ResponseCode.Success)
                Assert.Fail("An error occurred while executing the transaction.");

            var afterBalance = await this._myCheckingAccountService.GetBalance(1, 1);

            // Assert
            Assert.IsTrue((previousValue + request.Amount) == afterBalance);
        }

        [Test]
        public async Task DepositShouldFailDueNegativeAmount()
        {
            // Arrange
            var request = new DepositRequest()
            {
                AccountNumber = this.checkingAccount.AccountNumber,
                Amount = -10,
                Token = this.checkingAccount.Token
            };

            // Act
            var transaction = await _operationsService.Deposit(1, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("Amount"));
        }

        [Test]
        public async Task DepositShouldFailDueAnInvalidAccountNumber() 
        {
            // Arrange
            var request = new DepositRequest()
            {
                AccountNumber = string.Empty,
                Amount = 50,
                Token = this.checkingAccount.Token
            };

            // Act
            var transaction = await _operationsService.Deposit(1, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("AccountNumber"));
        }

        [Test]
        public async Task DepositShouldFailDueInvalidToken() 
        {
            // Arrange
            var request = new DepositRequest()
            {
                AccountNumber = this.checkingAccount.AccountNumber,
                Amount = 50,
                Token = string.Empty
            };

            // Act
            var transaction = await _operationsService.Deposit(1, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("Token"));

        }
    }
}
