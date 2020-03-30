using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Web.Domain.CheckingAccount
{
    public class Deposit
    {
        public Deposit(Guid transactionGuid)
        {
            this.TransactionGuid = transactionGuid;
        }

        [Key]
        public int DepositId { get; set; }

        [Required]
        public Guid TransactionGuid { get; set; }

        [ForeignKey("TransactionGuid")]
        public virtual Transaction Transaction { get; set; }
    }
}
