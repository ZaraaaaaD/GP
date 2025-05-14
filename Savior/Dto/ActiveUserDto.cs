namespace Savior.Dto
{
    public class ActiveUserDto
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; } = "Logged In";
    }
}
