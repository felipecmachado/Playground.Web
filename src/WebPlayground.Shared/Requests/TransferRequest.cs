namespace WebPlayground.Shared.Requests
{
    public class TransferRequest : TransactionRequest
    {
        public string BankCode { get; set; }

        public string FullName { get; set; }

        public string PersonId { get; set; }

        public string BeneficiaryAccountNumber {get; set;}
    }
}