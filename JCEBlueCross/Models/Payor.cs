using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JCEBlueCross.Models
{
    public class Payor
    {
        [Required]
        [Key]
        public int PayorId { get; set; }

        [Required]
        [StringLength(50)]
        public string PayorName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string PayorAddress { get; set; } = string.Empty;

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

        public ICollection<PayorError> PayorErrors { get; set; } = new List<PayorError>();
    }
}
