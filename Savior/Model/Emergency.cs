using Savior.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Savior.Models
{
    public class Emergency
    {
        [Key]
        public int EmergencyID { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public EmergencyType Type { get; set; }

        public DateTime RequestTime { get; set; } = DateTime.UtcNow;  
    }
}
