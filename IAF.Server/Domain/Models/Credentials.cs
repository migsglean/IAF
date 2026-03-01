using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAF.Server.Domain.Models
{
    [Table("Credentials")]
    public class Credentials
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Credential_ID { get; set; }

        [Required] 
        [MaxLength(50)] 
        public string UserName { get; set; }

        [Required]
        [EmailAddress]            
        [MaxLength(255)]           
        public string EmailAddress { get; set; }

        [Required] 
        [MaxLength(255)] 
        public string Password { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
