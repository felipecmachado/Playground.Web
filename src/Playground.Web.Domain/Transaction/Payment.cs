using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Web.Domain.CheckingAccount
{
    public class Payment
    {
        public Payment(Guid transactionGuid, string barCode)
        {
            this.TransactionGuid = transactionGuid;
            this.BarCode = barCode;
        }

        [Key]
        public int PaymentId { get; set; }

        [Required]
        public Guid TransactionGuid { get; set; }

        [MaxLength(30)]
        public string BarCode { get; set; }

        [ForeignKey("TransactionGuid")]
        public virtual Transaction Transaction { get; set; }
    }
}
