using System;
using System.Threading.Tasks;
using Playground.Web.Business.Interfaces;
using Playground.Web.Data;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Infrastructure;
using Playground.Web.Responses;
using Playground.Web.Shared.Exceptions;
using Playground.Web.Shared.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Playground.Web.Business.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(BankContext context) : base(context)
        {
        }

        public async Task<Response<Deposit>> Deposit(int userId, DepositRequest request)
        {
            var response = new Response<Deposit>();

            try
            {
                if (request.Amount <= 0)
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus.AddError("Amount", "Amount should be higher than 0");
                    return response;
                }

                var account = await this.Context.CheckingAccounts.FirstOrDefaultAsync(x => x.UserId == userId && x.AccountNumber == request.AccountNumber);

                var validation = this.ValidateAccount(account, request.Token);

                if (validation.HasErrors())
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus = validation;
                    return response;
                }

                var transaction = await this.DoTransaction(TransactionType.Deposit, account, request.Amount);

                response.Item = await this.GetDepositReceipt(transaction);
                response.Code = ResponseCode.Success;
            }
            catch (BusinessException ex)
            {
                response.Code = ResponseCode.Error;
                response.ResponseStatus.AddError(ex.FieldName, ex.Message);
            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.Fatal;
                response.ResponseStatus.StackTrace = ex.StackTrace;
                response.ResponseStatus.AddError("A fatal error occurred", ex.Message);

                return response;
            }

            return response;
        }

        public async Task<Response<Payment>> PayBill(int userId, PaymentRequest request)
        {
            var response = new Response<Payment>();

            try
            {
                if (request.Amount <= 0)
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus.AddError("Amount", "Amount should be higher than 0");
                    return response;
                }

                if (string.IsNullOrEmpty(request.BarCode))
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus.AddError("BarCode", "Barcode is invalid");
                    return response;
                }

                var account = await this.Context.CheckingAccounts.FirstOrDefaultAsync(x => x.UserId == userId && x.AccountNumber == request.AccountNumber);

                var validation = this.ValidateAccount(account, request.Token);

                if (validation.HasErrors())
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus = validation;
                    return response;
                }

                var transaction = await this.DoTransaction(TransactionType.Payment, account, request.Amount);

                response.Item = await this.GetPaymentReceipt(transaction);
                response.Code = ResponseCode.Success;
            }
            catch (BusinessException ex)
            {
                response.Code = ResponseCode.Error;
                response.ResponseStatus.AddError(ex.FieldName, ex.Message);
            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.Fatal;
                response.ResponseStatus.StackTrace = ex.StackTrace;
                response.ResponseStatus.AddError("A fatal error occurred", ex.Message);

                return response;
            }

            return response;
        }

        public async Task<Response<Withdraw>> Withdraw(int userId, WithdrawRequest request)
        {
            var response = new Response<Withdraw>();

            try
            {
                if (request.Amount <= 0)
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus.AddError("Amount", "Amount should be higher than 0");
                    return response;
                }

                var account = await this.Context.CheckingAccounts.FirstOrDefaultAsync(x => x.UserId == userId && x.AccountNumber == request.AccountNumber);

                var validation = this.ValidateAccount(account, request.Token);

                if (validation.HasErrors())
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus = validation;
                    return response;
                }

                if (account.Balance < request.Amount)
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus.AddError("InsuficientFunds", "Insuficient funds");
                    return response;
                }

                var transaction = await this.DoTransaction(TransactionType.Withdraw, account, request.Amount);

                response.Item = await this.GetWithdrawReceipt(transaction);
                response.Code = ResponseCode.Success;
            }
            catch (BusinessException ex)
            {
                response.Code = ResponseCode.Error;
                response.ResponseStatus.AddError(ex.FieldName, ex.Message);
            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.Fatal;
                response.ResponseStatus.StackTrace = ex.StackTrace;
                response.ResponseStatus.AddError("A fatal error occurred", ex.Message);

                return response;
            }

            return response;
        }

        public async Task<Response<Transfer>> Transfer(int userId, TransferRequest request)
        {
            var response = new Response<Transfer>();

            try
            {
                var account = await this.Context.CheckingAccounts.FirstOrDefaultAsync(x => x.UserId == userId && x.AccountNumber == request.AccountNumber);

                var validation = this.ValidateAccount(account, request.Token);

                // validate benefeciary

                if (validation.HasErrors())
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus = validation;
                    return response;
                }

                await this.DoTransaction(TransactionType.Transfer, account, request.Amount);
            }
            catch (BusinessException ex)
            {
                response.Code = ResponseCode.Error;
                response.ResponseStatus.AddError(ex.FieldName, ex.Message);
            }
            catch (Exception ex)
            {
                response.Code = ResponseCode.Fatal;
                response.ResponseStatus.StackTrace = ex.StackTrace;
                response.ResponseStatus.AddError("A fatal error occurred", ex.Message);

                return response;
            }

            return response;
        }

        private async Task<Guid> DoTransaction(TransactionType type, CheckingAccount account, decimal amount)
        {
            var previousTransaction = this.Context.Transactions
                .Where(x => x.CheckingAccountId == account.CheckingAccountId)
                .OrderBy(x => x.Timestamp)
                .Select(t => t.TransactionGuid).FirstOrDefaultAsync();

            switch (type)
            {
                case TransactionType.Withdraw:
                case TransactionType.Payment:
                case TransactionType.Transfer:
                    account.Balance -= amount;
                    break;
                case TransactionType.Deposit:
                    account.Balance += amount;
                    break;
                default:
                    break;
            }

            var transaction = new Transaction
            {
                CheckingAccountId = account.CheckingAccountId,
                Amount = amount,
                Balance = account.Balance,
                Timestamp = DateTime.UtcNow,
                PreviousTransaction = await previousTransaction,
                TransactionType = type,
            };

            using (var tran = Context.Database.BeginTransaction())
            {
                try
                {
                    this.Context.Transactions.Add(transaction);

                    this.Context.SaveChanges();

                    #region Generate receipt

                    switch (type)
                    {
                        case TransactionType.Withdraw:
                        case TransactionType.Payment:
                        case TransactionType.Transfer:
                            break;
                        case TransactionType.Deposit:
                            this.Context.Deposits.Add(new Deposit(transaction.TransactionGuid));
                            break;
                        default:
                            break;
                    }

                    this.Context.SaveChanges();

                    #endregion

                    // Update balance
                    this.Context.CheckingAccounts.Update(account);

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

            return transaction.TransactionGuid;
        }

        public async Task<Deposit> GetDepositReceipt(Guid transactionGuid)
            => await this.Context.Deposits.Where(x => x.TransactionGuid == transactionGuid).FirstOrDefaultAsync();

        public async Task<Withdraw> GetWithdrawReceipt(Guid transactionGuid)
            => await this.Context.Withdraws.Where(x => x.TransactionGuid == transactionGuid).FirstOrDefaultAsync();

        public async Task<Payment> GetPaymentReceipt(Guid transactionGuid)
            => await this.Context.Payments.Where(x => x.TransactionGuid == transactionGuid).FirstOrDefaultAsync();
    }
}
