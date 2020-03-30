namespace WebPlayground.Shared.Requests
{
    public abstract class TransactionRequest
    {
        public string AccountNumber { get; set; }

        public string Token { get; set; }

        public decimal Amount { get; set; }
    }
}