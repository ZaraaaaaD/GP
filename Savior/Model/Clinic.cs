using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savior.Models
{
    public class Clinic
    {
        [Key]
        public int ClinicID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Street { get; set; } = string.Empty;

        [Required]
        public int BuildingNumber { get; set; }


        public ICollection<WorksAt> WorksAts { get; set; }
    }
}