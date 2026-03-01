using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAF.Server.Domain.Models
{
    [Table("Parts")]
    public class Parts
    {
        [Key]
        [MaxLength(50)]
        public string Parts_ID { get; set; } 

        [Required]
        [MaxLength(255)]
        public string Parts_Desc { get; set; } 

        [Required]
        public int Quantity { get; set; }

        public byte[]? Image { get; set; }

        [Required]
        [MaxLength(50)]
        public string Product_ID { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
