using System.ComponentModel.DataAnnotations;

namespace Savior.Models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Feedback { get; set; }
    }
}
