using System.ComponentModel.DataAnnotations;

namespace WebPlayground.Shared.Requests
{
    public class AccountCreationRequest
    {
        [Required, MinLength(6), MaxLength(6)]
        public string Login { get; set; }

        [Required]
        public int UserId { get; set; }

        public string BranchCode { get; set; }
    }
}
