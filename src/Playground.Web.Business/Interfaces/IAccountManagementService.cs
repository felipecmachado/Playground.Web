using System.Collections.Generic;
using System.Threading.Tasks;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Shared.Requests;

namespace Playground.Web.Business.Interfaces
{
    public interface IAccountManagementService
    {
        Task<IList<CheckingAccount>> GetAllCheckingAccounts();
        Task<CheckingAccount> GetCheckingAccount(int id);
        Task CreateCheckingAccount(AccountCreationRequest request);
        Task DisableCheckingAccount(int checkingAccountId);
        Task<Balance> SaveBalance(int checkingAccountId);
        Task SaveBalanceForAllAccounts();
    }
}
