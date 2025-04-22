namespace Savior.Dto
{
    public class DoctorDTO
    {
        public int DoctorID { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public decimal BookingPrice { get; set; }
        public double? Rating { get; set; }

        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string ClinicPhone { get; set; }
        public List<string> WeeklyDays { get; set; }
        public List<string> DailyDays { get; set; }
        public string DailyTime { get; set; }
    }
}
