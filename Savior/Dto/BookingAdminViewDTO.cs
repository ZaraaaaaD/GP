namespace Savior.Dto
{
    public class BookingAdminViewDTO
    {
        public string UserName { get; set; }
        public string DoctorName { get; set; }
        public string PhoneNumber { get; set; }
        public string ClinicLocation { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string PaymentMethod { get; set; }
        public decimal BookingPrice { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
