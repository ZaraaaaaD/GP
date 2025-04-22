using Savior.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savior.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string MedicalLicenseNumber { get; set; } = string.Empty;

        [Required]
        public string Specialty { get; set; } = string.Empty;

        [Required]
        public string HospitalName { get; set; } = string.Empty;

        [Required]
        public string HospitalAddress { get; set; } = string.Empty;

        public string City { get; set; }

        public string PhoneNumber { get; set; }  
        public string SSN { get; set; }          
        public string ClinicName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public double Rating { get; set; }

        [Required]
        public decimal BookingPrice { get; set; }
        public bool IsApproved { get; set; } = false;
        public string ImageUrl { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 

        public ICollection<WeeklySchedule> WeeklySchedules { get; set; }
        public ICollection<DailySchedule> DailySchedules { get; set; }



        public ICollection<WorksAt> WorksAts { get; set; }
    }
}   