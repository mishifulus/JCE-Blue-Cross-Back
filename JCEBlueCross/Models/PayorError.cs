using System.ComponentModel.DataAnnotations;

namespace JCEBlueCross.Models
{
    public class PayorError
    {
        [Key]
        public int PayorErrorId { get; set; }

        public int ErrorId { get; set; }

        public int PayorId { get; set; }
    }
}
