using System;
using System.ComponentModel.DataAnnotations;

namespace WebPlayground.Domain.CheckingAccount
{
    public class Deposit
    {
        [Key]
        public Guid DepositId { get; set; }
    }
}
