namespace Playground.Web.Shared.Requests
{
    public class PaymentRequest : TransactionRequest
    {
        public string BarCode { get; set; }
    }
}