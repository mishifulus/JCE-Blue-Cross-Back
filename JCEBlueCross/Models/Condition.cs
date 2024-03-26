using System.ComponentModel.DataAnnotations;

namespace JCEBlueCross.Models
{
    public class Condition
    {
        [Required]
        [Key]
        public int ConditionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Field { get; set; } = string.Empty;

        [Required]
        public int ConditionLabel { get; set; } = 1; // 0 - Equal to, 1- More or equal to, 2- Less or equal to, 3-Not equal to, 4- In, 5- Between

        [Required]
        [StringLength(100)]
        public string Value { get; set; } = string.Empty;
    }
}
