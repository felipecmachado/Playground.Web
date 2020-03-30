using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Playground.Web.Business.Interfaces;
using Playground.Web.Data;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Infrastructure;
using Playground.Web.Responses;
using Playground.Web.Shared.Requests;

namespace Playground.Web.Business.Services
{
    public class AccountManagementService : BaseService, IAccountManagementService
    {
        public AccountManagementService(BankContext context) : base(context) { }

        public Task CreateCheckingAccount(AccountCreationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DisableCheckingAccount(int checkingAccountId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<CheckingAccount>> GetAllCheckingAccounts()
            => await this.Context.CheckingAccounts
                .Include(account => account.Branch)
                .Include(account => account.User)
                .ToListAsync();

        public async Task<CheckingAccount> GetCheckingAccount(int id)
            => await this.Context.CheckingAccounts
                .Include(account => account.Branch)
                .Include(account => account.User)
                .FirstOrDefaultAsync(x => x.CheckingAccountId == id);

        /// <summary>
        /// Takes a screenshot of the actual balance of an account
        /// </summary>
        /// <param name="checkingAccountId"></param>
        /// <returns></returns>
        public async Task<Balance> SaveBalance(int checkingAccountId)
        {
            var account = await this.Context.CheckingAccounts.FirstOrDefaultAsync(x => x.CheckingAccountId == checkingAccountId);

            var balance = this.GenerateBalance(account);

            this.Context.Add(balance);
            await this.Context.SaveChangesAsync();

            return balance;
        }

        /// <summary>
        /// Takes a screenshot of the actual balance of all accounts
        /// </summary>
        /// <param name="checkingAccountId"></param>
        /// <returns></returns>
        public async Task SaveBalanceForAllAccounts()
        {
            foreach(var account in this.Context.CheckingAccounts.Where(x => x.Enabled).ToList())
            {
                var balance = this.GenerateBalance(account);
                this.Context.Add(balance);
            }

            await this.Context.SaveChangesAsync();
        }

        private Balance GenerateBalance(CheckingAccount account)
        {
            var balance = new Balance()
            {
                CheckingAccountId = account.CheckingAccountId,
                Amount = account.Balance,
                Timestamp = DateTime.UtcNow
            };

            return balance;
        }
    }
}
