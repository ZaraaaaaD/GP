using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savior.Models
{
    public class FirstAid
    {
        [Key]
        public int CaseID { get; set; }

        [Required]
        public int UserID { get; set; }
        public User User { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Symptoms { get; set; }
        [Required]
        public string Steps { get; set; } = string.Empty;
    }
}