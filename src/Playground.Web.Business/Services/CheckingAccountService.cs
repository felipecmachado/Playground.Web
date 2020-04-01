using Microsoft.EntityFrameworkCore;
using Playground.Web.Business.Interfaces;
using Playground.Web.Data;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground.Web.Business.Services
{
    public class CheckingAccountService : BaseService, ICheckingAccountService
    {
        public CheckingAccountService(BankContext context) : base(context) { }

        public async Task<decimal> GetBalance(int userId, int accountId)
            => await this.Context.CheckingAccounts
                .Where(x => x.CheckingAccountId == accountId && x.UserId == userId)
                .Select(x => x.Balance)
                .FirstOrDefaultAsync();

        public async Task<IList<Balance>> GetBalances(int userId, int accountId, int days)
            => await this.Context.Balances
                .Where(x => x.CheckingAccountId == accountId && x.CheckingAccount.User.UserId == userId)
                .Where(x => x.Timestamp > DateTime.UtcNow.AddDays(-days))
                .OrderBy(x => x.Timestamp)
                .ToListAsync();

        public async Task<CheckingAccount> GetCheckingAccount(int userId, int accountId)
            => await this.Context.CheckingAccounts
                .Where(x => x.CheckingAccountId == accountId && x.UserId == userId)
                .FirstOrDefaultAsync();

        public async Task<IList<Transaction>> GetTransactions(int userId, int accountId, int days = 7)
            => await this.Context.Transactions
                .Where(x => x.CheckingAccountId == accountId && x.CheckingAccount.UserId == userId)           
                .Where(x => x.Timestamp > DateTime.UtcNow.AddDays(-days))
                .Take(10)
                .ToListAsync();
    }
}
