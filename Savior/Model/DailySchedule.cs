using System;

namespace Savior.Models
{
    public class DailySchedule
    {
        public int ID { get; set; }
        public int DoctorID { get; set; }

        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // إضافة ClinicID لربط الجدول بالعيادة
        public int ClinicID { get; set; }

        // العلاقة مع العيادة
        public Clinic Clinic { get; set; }
    }
}
