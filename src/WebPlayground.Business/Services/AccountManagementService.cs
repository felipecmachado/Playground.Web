using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPlayground.Business.Interfaces;
using WebPlayground.Data;
using WebPlayground.Domain.CheckingAccount;
using WebPlayground.Infrastructure;
using WebPlayground.Responses;
using WebPlayground.Shared.Requests;

namespace WebPlayground.Business.Services
{
    public class AccountManagementService : BaseService, IAccountManagementService
    {
        public AccountManagementService(BankContext context) : base(context) { }

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

        public async Task CreateAccount(AccountCreationRequest request)
        {

        }

        public async Task DisableAccount(int checkingAccountId)
        {

        }

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
