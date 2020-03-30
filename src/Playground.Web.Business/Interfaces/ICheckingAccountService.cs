using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playground.Web.Business.Interfaces
{
    public interface ICheckingAccountService
    {
        Task<Response<IList<Transaction>>> GetRecentTransactions(int userId, int accountId, int days = 7);
        Task<Response<IList<Transaction>>> GetStatement(int userId, int accountId);
        Task<decimal> GetBalance(int userId, int accountId);
        Task<CheckingAccount> GetCheckingAccount(int userId, int accountId);
    }
}
