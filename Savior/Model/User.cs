using Savior.Models;

public class User
{
    public int Id { get; set; }
    public string Fname { get; set; }  
    public string Lname { get; set; }  
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public string? ResetCode { get; set; }  
    public DateTime? ResetCodeExpiry { get; set; }
    public ICollection<Booking> Bookings { get; set; }
    public ICollection<Emergency> EmergencyRequests { get; set; }
    public ICollection<MedicalTeam> MedicalTeams { get; set; }
    public ICollection<FirstAid> FirstAids { get; set; }
}
