namespace Playground.Web.Shared.Requests
{
    public abstract class TransactionRequest
    {
        public int CheckingAccountId { get; set; }

        public string TransactionToken { get; set; }

        public decimal Amount { get; set; }
    }
}