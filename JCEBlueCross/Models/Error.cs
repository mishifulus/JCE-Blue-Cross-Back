﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JCEBlueCross.Models
{
    public class Error
    {
        [Required]
        [Key]
        public int ErrorId { get; set; }

        [Required]
        [StringLength(60)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Field { get; set; } = string.Empty;

        [Required]
        public int Status { get; set; } = 1; // 1 - Active, 0 - Inactive

        [Column(TypeName = "DATETIME")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public User? RegisteringUser { get; set; }
    }
}
