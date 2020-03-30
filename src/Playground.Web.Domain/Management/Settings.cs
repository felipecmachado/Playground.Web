using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Web.Domain.Management
{
    public class Settings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingsId { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        public string Key { get; set; }

        [Required]
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
