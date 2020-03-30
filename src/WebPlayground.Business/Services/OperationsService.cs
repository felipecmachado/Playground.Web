using System;
using System.Threading.Tasks;
using WebPlayground.Business.Interfaces;
using WebPlayground.Data;
using WebPlayground.Domain.CheckingAccount;
using WebPlayground.Infrastructure;
using WebPlayground.Responses;
using WebPlayground.Shared.Exceptions;
using WebPlayground.Shared.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebPlayground.Business.Services
{
    public class OperationsService : BaseService, IOperationsService
    {
        public OperationsService(BankContext context) : base(context)
        {
        }

        public async Task<Response<Deposit>> Deposit(int userId, DepositRequest request)
        {
            var response = new Response<Deposit>();

            try
            {
                var account = await this.Context.CheckingAccounts.FirstOrDefaultAsync(x => x.UserId == userId && x.AccountNumber == request.AccountNumber);

                var validation = this.ValidateAccount(account, request.Token);
                
                if(validation.Errors.Any())
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus = validation;
                    return response;
                }

                await this.DoTransaction(TransactionType.Deposit, account.CheckingAccountId, account.Balance, request.Amount);
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
                var account = await this.Context.CheckingAccounts.FirstOrDefaultAsync(x => x.UserId == userId && x.AccountNumber == request.AccountNumber);

                var validation = this.ValidateAccount(account, request.Token);

                if (validation.Errors.Any())
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus = validation;
                    return response;
                }

                await this.DoTransaction(TransactionType.Payment, account.CheckingAccountId, account.Balance, request.Amount);
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
                var account = await this.Context.CheckingAccounts.FirstOrDefaultAsync(x => x.UserId == userId && x.AccountNumber == request.AccountNumber);

                var validation = this.ValidateAccount(account, request.Token);

                if (validation.Errors.Any())
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus = validation;
                    return response;
                }

                await this.DoTransaction(TransactionType.Withdraw, account.CheckingAccountId, account.Balance, request.Amount);
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

                if (validation.Errors.Any())
                {
                    response.Code = ResponseCode.Error;
                    response.ResponseStatus = validation;
                    return response;
                }

                await this.DoTransaction(TransactionType.Transfer, account.CheckingAccountId, account.Balance, request.Amount);
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

        private ResponseStatus ValidateAccount(CheckingAccount account, string token)
        {
            var response = new ResponseStatus();

            if (account == null)
            {
                response.AddError("AccountNumber", "The account number is invalid.");
                return response;
            }

            if (!account.IsTokenValid(token))
            {
                // TODO: After three invalid tokens it should block the account for 15 minutes
                response.AddError("Token", "Token is invalid.");
                return response;
            }

            return response;
        }

        private async Task DoTransaction(TransactionType type, int checkingAccountId, decimal balance, decimal amount)
        {
            var previousTransaction = this.Context.Transactions
                .Where(x => x.CheckingAccountId == checkingAccountId)
                .OrderBy(x => x.Timestamp)
                .Select(t => t.TransactionGuid).FirstOrDefaultAsync();

            switch (type)
            {   
                case TransactionType.Withdraw:
                case TransactionType.Payment:
                case TransactionType.Transfer:
                    balance =- amount;
                    break;
                case TransactionType.Deposit:
                    balance =+ amount;
                    break;
                default:
                    break;
            }

            var transaction = new Transaction
            {
                CheckingAccountId = checkingAccountId,
                Amount = amount,
                Balance = balance,
                Timestamp = DateTime.UtcNow,
                PreviousTransaction = await previousTransaction,
                TransactionType = type,
            };

            this.Context.Transactions.Add(transaction);

            await this.Context.SaveChangesAsync();
        }
    }
}
