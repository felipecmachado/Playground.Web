using System.Threading.Tasks;

namespace WebPlayground.Business.Interfaces
{
    public interface IMyCheckingAccountService
    {
        Task<bool> GetRecentTransactions();
        Task<bool> GetStatement();
        Task<bool> GetBalance();
        Task<bool> GetCheckingAccount();
        Task<bool> AddCheckingAccount();
        Task<bool> UpdateCheckingAccount();
    }
}
