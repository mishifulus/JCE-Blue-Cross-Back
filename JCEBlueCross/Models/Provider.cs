using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JCEBlueCross.Models
{
    public class Provider
    {
        [Required]
        [Key]
        public int ProviderId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProviderName { get; set; } = string.Empty;

        [Required]
        public int Type { get; set; } = 0; // 0 - Institutional, 1 - Professional

        [Required]
        [StringLength(50)]
        public string ProviderAddress { get; set; } = string.Empty;

        [Required]
        [StringLength(5)]
        public string ZipCode { get; set; } = string.Empty;

        [Required]
        [StringLength(3)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string City { get; set; } = string.Empty;

        [Required]
        public int Status { get; set; } = 1; // 1 - Active, 0 - Inactive

        [Column(TypeName = "DATETIME")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public User? RegisteringUser { get; set; }
    }
}
