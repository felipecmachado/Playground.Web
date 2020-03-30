using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Playground.Web.Business.Interfaces;
using Playground.Web.Data;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Infrastructure;
using Playground.Web.Responses;

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

        public async Task<CheckingAccount> GetCheckingAccount(int userId, int accountId)
            => await this.Context.CheckingAccounts
                .Where(x => x.CheckingAccountId == accountId && x.UserId == userId)
                .FirstOrDefaultAsync();

        public Task<Response<IList<Transaction>>> GetRecentTransactions(int userId, int accountId, int days = 7)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IList<Transaction>>> GetStatement(int userId, int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
