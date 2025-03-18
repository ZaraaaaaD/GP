using Savior.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Savior.Models
{
    public class MedicalTeam
    {
        [Key]
        public int TeamID { get; set; }

        [Required]
        public MedicalRole Role { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;  

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public DateTime RequestTime { get; set; } = DateTime.UtcNow;  

        public TimeSpan? Duration { get; set; }  
    }
}
