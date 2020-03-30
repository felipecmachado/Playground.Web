using NUnit.Framework;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Shared.Requests;
using System.Threading.Tasks;

namespace Playground.Web.Tests.UnitTests
{
    public class PaymentTests : BaseTests
    {
        private const int USER_ID = 1;
        private const int ACCOUNT_ID = 1;

        private CheckingAccount checkingAccount;

        public PaymentTests()
        {
        }

        [SetUp]
        public async Task Setup()
        {
            this.checkingAccount = (await this._myCheckingAccountService.GetCheckingAccount(USER_ID, ACCOUNT_ID));
        }

        [Test]
        public async Task PaymentMustBeSuccessful() 
        {
            // Arrange
            var request = new PaymentRequest()
            { 
                AccountNumber = this.checkingAccount.AccountNumber,
                Amount = 50,
                Token = this.checkingAccount.Token,
                BarCode = "0000000000000000000"
            };

            // Act
            var previousValue = await this._myCheckingAccountService.GetBalance(1, 1);

            var transaction = await _operationsService.PayBill(1, request);

            if (transaction.Code != Responses.ResponseCode.Success)
                Assert.Fail("An error occurred while executing the transaction.");

            var afterBalance = await this._myCheckingAccountService.GetBalance(1, 1);

            // Assert
            Assert.IsTrue((previousValue - request.Amount) == afterBalance);
        }

        [Test]
        public async Task PaymentShouldFailDueNegativeAmount()
        {
            // Arrange
            var request = new PaymentRequest()
            {
                AccountNumber = this.checkingAccount.AccountNumber,
                Amount = -10,
                Token = this.checkingAccount.Token
            };

            // Act
            var transaction = await _operationsService.PayBill(1, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("Amount"));
        }

        [Test]
        public async Task PaymentShouldFailDueAnInvalidAccountNumber() 
        {
            // Arrange
            var request = new PaymentRequest()
            {
                AccountNumber = string.Empty,
                Amount = 50,
                Token = this.checkingAccount.Token,
                BarCode = "000000000000000000"
            };

            // Act
            var transaction = await _operationsService.PayBill(1, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("AccountNumber"));
        }

        [Test]
        public async Task PaymentShouldFailDueInvalidToken() 
        {
            // Arrange
            var request = new PaymentRequest()
            {
                AccountNumber = this.checkingAccount.AccountNumber,
                Amount = 50,
                Token = string.Empty,
                BarCode = "000000000000000000"
            };

            // Act
            var transaction = await _operationsService.PayBill(1, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("Token"));

        }
    }
}
