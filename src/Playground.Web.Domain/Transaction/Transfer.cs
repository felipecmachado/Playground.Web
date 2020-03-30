using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Web.Domain.CheckingAccount
{
    public class Transfer
    {
        [Key]
        public int TransferId { get; set; }

        [Required]
        public Guid TransactionGuid { get; set; }

        [Required]
        public Guid BeneficiaryId { get; set; }

        public TransferStatus TransferStatus { get; set; } = TransferStatus.Waiting;

        [MaxLength(100)]
        public string BeneficiaryMessage { get; set; }

        [ForeignKey("TransactionGuid")]
        public virtual Transaction Transaction { get; set; }
    }
}
