using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Web.Domain.CheckingAccount
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionGuid { get; set; }

        [Required]
        public int CheckingAccountId { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The previous transaction that the user had made, making it easier to keep track of the transaction history
        /// In case 
        /// </summary>
        public Guid PreviousTransaction { get; set; }

        [MaxLength(50)]
        public string Message { get; set; }

        /// <summary>
        /// Balance after transaction succedded
        /// </summary>
        public virtual decimal Balance { get; set; }

        public virtual Transfer Transfer { get; set; }

        public virtual Deposit Deposit { get; set; }

        public virtual Withdraw Withdraw { get; set; }

        public virtual Payment Payment { get; set; }

    }
}
