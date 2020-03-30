using System.Threading.Tasks;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Responses;
using Playground.Web.Shared.Requests;

namespace Playground.Web.Business.Interfaces
{
    public interface ITransactionService
    {
        Task<Response<Withdraw>> Withdraw(int userId, WithdrawRequest request);

        Task<Response<Deposit>> Deposit(int userId, DepositRequest request);

        Task<Response<Payment>> PayBill(int userId, PaymentRequest request);

        Task<Response<Transfer>> Transfer(int userId, TransferRequest request);
    }
}