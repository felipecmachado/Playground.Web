using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPlayground.Domain.CheckingAccount
{
    public class Balance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BalanceId { get; set; }

        public int CheckingAccountId { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public DateTime Timestamp { get; set; }

        [ForeignKey("CheckingAccountId")]
        public virtual CheckingAccount CheckingAccount { get; set; }
    }
}
