using System;
using System.Threading.Tasks;
using WebPlayground.Business.Interfaces;
using WebPlayground.Data;
using WebPlayground.Infrastructure;

namespace WebPlayground.Business.Services
{
    public class MyCheckingAccountService : BaseService, IMyCheckingAccountService
    {
        public MyCheckingAccountService(BankContext context) : base(context) { }

        public Task<bool> AddCheckingAccount()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCheckingAccount()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetBalance()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetCheckingAccount()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetRecentTransactions()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetStatement()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCheckingAccount()
        {
            throw new NotImplementedException();
        }
    }
}
