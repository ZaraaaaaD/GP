using Savior.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savior.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        public int UserID { get; set; }
        public User User { get; set; }

        [Required]
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        [Required]
        public DateTime patientBirthdate { get; set; }

        [Required]
        public string patientGender { get; set; } = string.Empty;
        [Required]
        public decimal BookingPrice { get; set; }
        [Required]
        public string bookingNumber { get; set; } = string.Empty;
        [Required]
        public DateTime bookingTime { get; set; }
        [Required]
        public string receipt { get; set; } = string.Empty;


        [Required]
        public PaymentForBooking paymentMethod { get; set; }
    }

}