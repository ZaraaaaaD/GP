using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savior.Models
{
    public class WorksAt
    {
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        [Required]
        public TimeOnly ShiftTiming { get; set; }
    }
}