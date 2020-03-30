using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPlayground.Domain.Management
{
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required, MinLength(5), MaxLength(100)]
        public string FirstLine { get; set; }

        [MaxLength(100)]
        public string SecondLine { get; set; }

        [Required, MinLength(2), MaxLength(2)]
        public string Province { get; set; }

        [Required]
        public string City { get; set; }

        [ForeignKey("CustomerId")]
        public virtual User Customer { get; set; }
    }
}
