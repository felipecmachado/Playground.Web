using System.Threading.Tasks;
using WebPlayground.Domain.CheckingAccount;
using WebPlayground.Responses;
using WebPlayground.Shared.Requests;

namespace WebPlayground.Business.Interfaces
{
    public interface IOperationsService
    {
        Task<Response<Withdraw>> Withdraw(int userId, WithdrawRequest request);

        Task<Response<Deposit>> Deposit(int userId, DepositRequest request);

        Task<Response<Payment>> PayBill(int userId, PaymentRequest request);

        Task<Response<Transfer>> Transfer(int userId, TransferRequest request);
    }
}