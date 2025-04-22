using System;

namespace Savior.Models
{
    public class WeeklySchedule
    {
        public int ID { get; set; }
        public int DoctorID { get; set; }
        public string DayOfWeek { get; set; }

        // إضافة ClinicID لربط الجدول بالعيادة
        public int ClinicID { get; set; }

        // العلاقة مع العيادة
        public Clinic Clinic { get; set; }
    }
}
