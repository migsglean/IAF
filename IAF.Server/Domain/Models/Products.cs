using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAF.Server.Domain.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        [MaxLength(50)]
        public string Product_ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Product_Desc { get; set; }

        [Required]
        public int Forecasted_Produced_Count { get; set; }

        public byte[]? Image { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}