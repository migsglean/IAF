using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAF.Server.Domain.Models
{
    [Table("Production_Summary")]
    public class ProductionSummary
    {
        [Key]
        [MaxLength(50)]
        public string TransactionNo { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Product_ID")]
        public string ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Parts_ID")]
        public string PartsId { get; set; }

        [Required]
        [MaxLength(255)]
        public string TransactedBy { get; set; }

        [Required]
        public DateTime Transaction_Date { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}