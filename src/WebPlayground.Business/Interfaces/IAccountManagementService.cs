using System.Collections.Generic;
using System.Threading.Tasks;
using WebPlayground.Domain.CheckingAccount;

namespace WebPlayground.Business.Interfaces
{
    public interface IAccountManagementService
    {
        Task<IList<CheckingAccount>> GetAllCheckingAccounts();
        Task<CheckingAccount> GetCheckingAccount(int id);
        Task<Balance> SaveBalance(int checkingAccountId);
        Task SaveBalanceForAllAccounts();
    }
}
