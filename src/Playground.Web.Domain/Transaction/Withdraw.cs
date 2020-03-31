using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Web.Domain.CheckingAccount
{
    public class Withdraw
    {
        public Withdraw(Guid transactionGuid, string address)
        {
            this.TransactionGuid = transactionGuid;
            this.Address = address;
        }

        [Key]
        public int WithdrawId { get; set; }

        [Required]
        public Guid TransactionGuid { get; set; }

        /// <summary>
        /// Address where the withdraw occurred
        /// </summary>
        [MaxLength(100)]
        public string Address { get; set; }

        [ForeignKey("TransactionGuid")]
        public virtual Transaction Transaction { get; set; }
    }
}
