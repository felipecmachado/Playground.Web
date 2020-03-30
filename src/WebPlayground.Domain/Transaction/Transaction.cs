using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPlayground.Domain.CheckingAccount
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


    }
}
