using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JCEBlueCross.Models
{
    public class User
    {
        [Required]
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string UserAddress { get; set; } = string.Empty;

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
        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yy}")]
        public DateTime DOB { get; set; } = Convert.ToDateTime("01/01/2000");

        [Required]
        public int Sex { get; set; } = 0; // 0 = H, 1 = M, 2 = O

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int Role { get; set; } = 0; // 0 - Admin, 1 - Member, 2 - Provider, 3 - Payor

        [Required]
        public int Status { get; set; } = 1; // 1 - New Password, 0 - Blocked, 2 - Active

        [Column(TypeName = "DATETIME")]
        public DateTime SubscribedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        public DateTime ExpireDate { get; set; } = DateTime.Now;

        public string MotherQuestion { get; set; } = string.Empty;

        public string ChilhoodQuestion { get; set; } = string.Empty;

        public string CityQuestion { get; set; } = string.Empty;

        public string CarQuestion { get; set; } = string.Empty;

        public string UniversityQuestion { get; set; } = string.Empty;

        public string SportQuestion { get; set; } = string.Empty;

        public string BossQuestion { get; set; } = string.Empty;

        public string BandQuestion { get; set; } = string.Empty;
    }
}
