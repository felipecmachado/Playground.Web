using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Playground.Web.Domain.CheckingAccount;

namespace Playground.Web.Domain.Management
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required, MinLength(6), MaxLength(6)]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public DateTime LastAccessAt { get; set; }

        public virtual CheckingAccount.CheckingAccount CheckingAccount { get; set; }
    }
}
