using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playground.Web.Business.Interfaces
{
    public interface ICheckingAccountService
    {
        Task<IList<Transaction>> GetTransactions(int userId, int accountId, int days = 7);
        Task<decimal> GetBalance(int userId, int accountId);
        Task<CheckingAccount> GetCheckingAccount(int userId, int accountId);
        Task<IList<Balance>> GetBalances(int userId, int accountId, int days);
    }
}
