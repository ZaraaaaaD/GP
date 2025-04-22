namespace Savior.Dto
{
    public class BookingDetailsDto
    {
        public int BookingID { get; set; }
        public string UserName { get; set; }
        public string DoctorName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BookingPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string ClinicAddress { get; set; }
    }
}
