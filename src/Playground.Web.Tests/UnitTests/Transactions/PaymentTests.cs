using NUnit.Framework;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Shared.Requests;
using System.Threading.Tasks;

namespace Playground.Web.Tests.UnitTests
{
    public class PaymentTests : BaseTests
    {
        private const int USER_ID = 1;

        private CheckingAccount checkingAccount;

        public PaymentTests()
        {
        }

        [SetUp]
        public async Task Setup()
        {
            var user = await this._userService.GetById(USER_ID);
            this.checkingAccount = (await this._myCheckingAccountService.GetCheckingAccount(USER_ID, user.Item.CheckingAccount.CheckingAccountId));
        }

        [Test]
        public async Task PaymentMustBeSuccessful() 
        {
            // Arrange
            var request = new PaymentRequest()
            { 
                CheckingAccountId = this.checkingAccount.CheckingAccountId,
                Amount = 50,
                TransactionToken = this.checkingAccount.TransactionToken,
                BarCode = "0000000000000000000"
            };

            // Act
            var previousValue = await this._myCheckingAccountService.GetBalance(USER_ID, this.checkingAccount.CheckingAccountId);

            var transaction = await _operationsService.PayBill(USER_ID, request);

            if (transaction.Code != Responses.ResponseCode.Success)
                Assert.Fail("An error occurred while executing the transaction.");

            var afterBalance = await this._myCheckingAccountService.GetBalance(USER_ID, this.checkingAccount.CheckingAccountId);

            // Assert
            Assert.IsTrue((previousValue - request.Amount) == afterBalance);
        }

        [Test]
        public async Task PaymentShouldFailDueInsuficientFunds()
        {
            // Arrange
            var request = new PaymentRequest()
            {
                CheckingAccountId = this.checkingAccount.CheckingAccountId,
                Amount = 750,
                TransactionToken = this.checkingAccount.TransactionToken,
                BarCode = "0000000000000000000"
            };

            // Act
            var transaction = await _operationsService.PayBill(USER_ID, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("InsuficientFunds"));
        }

        [Test]
        public async Task PaymentShouldFailDueNegativeAmount()
        {
            // Arrange
            var request = new PaymentRequest()
            {
                CheckingAccountId = this.checkingAccount.CheckingAccountId,
                Amount = -10,
                TransactionToken = this.checkingAccount.TransactionToken
            };

            // Act
            var transaction = await _operationsService.PayBill(USER_ID, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("Amount"));
        }

        [Test]
        public async Task PaymentShouldFailDueAnInvalidAccount() 
        {
            // Arrange
            var request = new PaymentRequest()
            {
                CheckingAccountId = 0,
                Amount = 50,
                TransactionToken = this.checkingAccount.TransactionToken,
                BarCode = "000000000000000000"
            };

            // Act
            var transaction = await _operationsService.PayBill(USER_ID, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("AccountNumber"));
        }

        [Test]
        public async Task PaymentShouldFailDueInvalidToken() 
        {
            // Arrange
            var request = new PaymentRequest()
            {
                CheckingAccountId = this.checkingAccount.CheckingAccountId,
                Amount = 50,
                TransactionToken = string.Empty,
                BarCode = "000000000000000000"
            };

            // Act
            var transaction = await _operationsService.PayBill(USER_ID, request);

            // Assert
            Assert.IsTrue(transaction.Code == Responses.ResponseCode.Error && transaction.ResponseStatus.HasError("Token"));

        }
    }
}
