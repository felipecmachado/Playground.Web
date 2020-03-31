using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Playground.Web.Domain.Management;
using Playground.Web.Domain.Branch;

namespace Playground.Web.Domain.CheckingAccount
{
    public class CheckingAccount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CheckingAccountId { get; set; }

        public int BranchId { get; set; }

        public string AccountNumber { get; set; }

        [Required]
        public string TransactionToken { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public decimal Balance { get; set; } = 0;

        [Required]
        public bool AutomaticInterest { get; set; } = true;

        [Required]
        public bool Enabled { get; set; } = true;

        [ForeignKey("BranchId")]
        public virtual Branch.Branch Branch { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public bool IsTokenValid(string token)
            => this.TransactionToken == token;

        public void GenerateToken()
        {
            this.TransactionToken = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
        }
    }
}
