using System;
using System.Collections.Generic;
using System.Text;

namespace WebPlayground.Shared.Requests
{
    public class PaymentRequest : TransactionRequest
    {
        public string BarCode { get; set; }
    }
}