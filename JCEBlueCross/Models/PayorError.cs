using System.ComponentModel.DataAnnotations;

namespace JCEBlueCross.Models
{
    public class PayorError
    {
        [Key]
        public int PayorErrorId { get; set; }

        public Error Error { get; set; }

        public Payor Payor { get; set; }
    }
}
