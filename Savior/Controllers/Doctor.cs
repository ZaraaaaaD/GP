using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savior.Data;
using Savior.Dto;
using Savior.Enumerations;
using Savior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Savior.Dtos;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/doctor")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DoctorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterDoctor([FromBody] DoctorRegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data.");

        var doctor = new Doctor
        {
            FirstName = dto.FName,
            LastName = dto.LName,
            PhoneNumber = dto.PhoneNumber,
            Specialty = dto.Specialty,
            MedicalLicenseNumber = dto.MedicalLicenseNumber,
            ClinicName = dto.ClinicName,
            City = dto.City,
            Email = dto.Email,
            SSN = dto.SSN,
            IsApproved = false
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        await SendRegistrationEmail(doctor);

        return Ok("Your registration request has been received and is under review.");
    }



    private async Task SendRegistrationEmail(Doctor doctor)
    {
        var fromAddress = new MailAddress("mzarad905@gmail.com", "Savior Team");
        var toAddress = new MailAddress(doctor.Email);
        const string subject = "Doctor Registration Request Received";

        string body = $"Dear Dr. {doctor.FirstName} {doctor.LastName},\n\n" +
                      "Your registration request has been received and is under review. " +
                      "We will notify you once the review process is complete.\n\n" +
                      "Best regards,\nSavior Team";

        using (var smtpClient = new SmtpClient("smtp.gmail.com"))
        {
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("mzarad905@gmail.com", "wjfl quzj sccw qgpr"); // App password
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }


    [HttpGet("specialties")]
    public ActionResult<IEnumerable<string>> GetAllSpecialties()
    {
        var specialties = new List<string>
    {
        "Dentist",
        "Orthopaedic",
        "Ear, Nose, and Throat",
        "Optometrists",
        "Cardiologist",
        "Pediatricians",
        "Gynecologist",
        "Physical Therapy",
        "Dermatologists",
        "Gastroenteritis",
        "Psychiatrists",
        "Top Rating"
    };

        return Ok(specialties);
    }




    [HttpGet("doctors/by-specialty")]
    public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetDoctorsBySpecialty(string specialty)
    {
        var doctors = await _context.Doctors
            .Where(d => d.IsApproved && d.Specialty == specialty)
            .Select(d => new DoctorDTO
            {
                DoctorID = d.DoctorID,
                FullName = d.FirstName + " " + d.LastName,
                Specialty = d.Specialty,
                HospitalName = d.HospitalName,
                HospitalAddress = d.HospitalAddress,
                ImageUrl = d.ImageUrl,
                Description = d.Description,
                City = d.City,
                BookingPrice = d.BookingPrice,
                Rating = d.Rating, 
                ClinicName = _context.Clinics
                    .Where(c => c.ClinicID == (d.WorksAts.Any() ? d.WorksAts.FirstOrDefault().ClinicID : 0))
                    .Select(c => c.Name)
                    .FirstOrDefault(),

                ClinicAddress = _context.Clinics
                    .Where(c => c.ClinicID == (d.WorksAts.Any() ? d.WorksAts.FirstOrDefault().ClinicID : 0))
                    .Select(c => c.Address)
                    .FirstOrDefault(),

                ClinicPhone = _context.Clinics
                    .Where(c => c.ClinicID == (d.WorksAts.Any() ? d.WorksAts.FirstOrDefault().ClinicID : 0))
                    .Select(c => c.Phone)
                    .FirstOrDefault(),

                WeeklyDays = _context.WeeklySchedules
                    .Where(ws => ws.DoctorID == d.DoctorID)
                    .Select(ws => ws.DayOfWeek)
                    .ToList(),

                DailyDays = _context.DailySchedules
                    .Where(ds => ds.DoctorID == d.DoctorID)
                    .Select(ds => ds.DayOfWeek)
                    .ToList(),

                DailyTime = _context.DailySchedules
                    .Where(ds => ds.DoctorID == d.DoctorID)
                    .Select(ds => ds.StartTime.ToString(@"hh\:mm") + " - " + ds.EndTime.ToString(@"hh\:mm"))
                    .FirstOrDefault()
            })
            .ToListAsync();

        return Ok(doctors);
    }




    [HttpGet("doctors")]

    public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetDoctors()

    {

        var doctors = await _context.Doctors

            .Where(d => d.IsApproved)

            .Select(d => new DoctorDTO

            {

                DoctorID = d.DoctorID,

                FullName = d.FirstName + " " + d.LastName,

                Specialty = d.Specialty,

                HospitalName = d.HospitalName,

                HospitalAddress = d.HospitalAddress,

                ImageUrl = d.ImageUrl,

                Description = d.Description,

                City = d.City,
                BookingPrice = d.BookingPrice,
                Rating = d.Rating,



                ClinicName = _context.Clinics

                    .Where(c => c.ClinicID == (d.WorksAts.Any() ? d.WorksAts.FirstOrDefault().ClinicID : 0))

                    .Select(c => c.Name)

                    .FirstOrDefault(),



                ClinicAddress = _context.Clinics

                    .Where(c => c.ClinicID == (d.WorksAts.Any() ? d.WorksAts.FirstOrDefault().ClinicID : 0))

                    .Select(c => c.Address)

                    .FirstOrDefault(),



                ClinicPhone = _context.Clinics

                    .Where(c => c.ClinicID == (d.WorksAts.Any() ? d.WorksAts.FirstOrDefault().ClinicID : 0)) // Check if WorksAts is not empty

                    .Select(c => c.Phone)

                    .FirstOrDefault(),





                WeeklyDays = _context.WeeklySchedules

                    .Where(ws => ws.DoctorID == d.DoctorID)

                    .Select(ws => ws.DayOfWeek)

                    .ToList(),



                DailyDays = _context.DailySchedules

                    .Where(ds => ds.DoctorID == d.DoctorID)

                    .Select(ds => ds.DayOfWeek)

                    .ToList(),



                DailyTime = _context.DailySchedules

                    .Where(ds => ds.DoctorID == d.DoctorID)

                    .Select(ds => ds.StartTime.ToString(@"hh\:mm") + " - " + ds.EndTime.ToString(@"hh\:mm"))

                    .FirstOrDefault()

            })

            .ToListAsync();



        return Ok(doctors);

    }



    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorDTO>> GetDoctorById(int id)
    {
        var doctor = await _context.Doctors
            .Where(d => d.DoctorID == id && d.IsApproved)
            .Select(d => new DoctorDTO
            {
                DoctorID = d.DoctorID,
                FullName = d.FirstName + " " + d.LastName,
                Specialty = d.Specialty,
                HospitalName = d.HospitalName,
                HospitalAddress = d.HospitalAddress,
                ImageUrl = d.ImageUrl,
                Description = d.Description,
                BookingPrice = d.BookingPrice, 
                City = d.City,
                Rating = d.Rating,


                ClinicName = _context.Clinics
                    .Where(c => c.ClinicID == (d.WorksAts.Any() ? d.WorksAts.FirstOrDefault().ClinicID : 0))
                    .Select(c => c.Name)
                    .FirstOrDefault(),

                ClinicAddress = _context.Clinics
                    .Where(c => c.ClinicID == (d.WorksAts.Any() ? d.WorksAts.FirstOrDefault().ClinicID : 0))
                    .Select(c => c.Address)
                    .FirstOrDefault(),

                ClinicPhone = _context.Clinics
                    .Where(c => c.ClinicID == (d.WorksAts.Any() ? d.WorksAts.FirstOrDefault().ClinicID : 0))
                    .Select(c => c.Phone)
                    .FirstOrDefault(),

                WeeklyDays = _context.WeeklySchedules
                    .Where(ws => ws.DoctorID == d.DoctorID)
                    .Select(ws => ws.DayOfWeek)
                    .ToList(),

                DailyDays = _context.DailySchedules
                    .Where(ds => ds.DoctorID == d.DoctorID)
                    .Select(ds => ds.DayOfWeek)
                    .ToList(),

                DailyTime = _context.DailySchedules
                    .Where(ds => ds.DoctorID == d.DoctorID)
                    .Select(ds => ds.StartTime.ToString(@"hh\:mm") + " - " + ds.EndTime.ToString(@"hh\:mm"))
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (doctor == null)
        {
            return NotFound();
        }

        return Ok(doctor);
    }






    [HttpGet("doctors/search")]
    public async Task<ActionResult<IEnumerable<Doctor>>> SearchDoctorsByName([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Please provide a search term.");

        var doctors = await _context.Doctors
            .Where(d =>
                d.FirstName.Contains(name) ||
                d.LastName.Contains(name) ||
                (d.FirstName + " " + d.LastName).Contains(name) ||
                d.Specialty.Contains(name))
            .ToListAsync();

        if (doctors == null || doctors.Count == 0)
            return NotFound("No doctors found matching the search term.");

        return doctors;
    }

    [Authorize]
    [HttpPost("book")]
    public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentDto dto)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.DoctorID == dto.DoctorId && d.IsApproved);

        if (doctor == null)
            return NotFound("Doctor not found.");

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
            return Unauthorized("Invalid token.");

        int userId = int.Parse(userIdClaim);
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return Unauthorized("User not found.");

        var schedule = await _context.DailySchedules
            .FirstOrDefaultAsync(s => s.DoctorID == dto.DoctorId && s.DayOfWeek == dto.Day);

        if (schedule == null)
            return BadRequest("No available schedule for this doctor on the selected day.");

        var worksAt = await _context.WorksAts
            .FirstOrDefaultAsync(w => w.DoctorID == dto.DoctorId);

        if (worksAt == null)
            return BadRequest("This doctor is not assigned to any clinic.");

        string startTimeFormatted = schedule.StartTime.ToString(@"hh\:mm");
        string endTimeFormatted = schedule.EndTime.ToString(@"hh\:mm");
        string displayTime = $"{startTimeFormatted} - {endTimeFormatted}";

        var booking = new Booking
        {
            UserID = userId,
            DoctorID = doctor.DoctorID,
            ClinicID = worksAt.ClinicID,
            BookingPrice = doctor.BookingPrice,
            Day = dto.Day,
            Time = displayTime,
            bookingTime = DateTime.Now,
            paymentMethod = PaymentForBooking.Cash
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        var response = new BookingResponseDto
        {
            UserName = user.Fname + " " + user.Lname,
            DoctorName = doctor.FirstName + " " + doctor.LastName,
            PhoneNumber = user.Phone,
            BookingPrice = doctor.BookingPrice,
            PaymentMethod = "Cash",
            Day = dto.Day,
            Time = displayTime,
            ClinicLocation = doctor.HospitalAddress
        };

        var fromAddress = new MailAddress("mzarad905@gmail.com", "Savior Team");
        var toAddress = new MailAddress(user.Email);

        string subject = "Appointment Booking Confirmation";
        string body = $"Dear {user.Fname} {user.Lname},\n\n" +
                      $"Your appointment has been successfully booked with Dr. {doctor.FirstName} {doctor.LastName}.\n\n" +
                      $"📅 Day: {dto.Day}\n" +
                      $"🕐 Time: {response.Time}\n" +
                      $"📍 Location: {response.ClinicLocation}\n" +
                      $"💵 Booking Price: {response.BookingPrice} EGP\n" +
                      $"💳 Payment Method: {response.PaymentMethod}\n" +
                      $"📞 Phone Number: {user.Phone}\n\n" +
                      "Thank you for using Savior!\n\n" +
                      "Best regards,\nSavior Team";

        using (var smtpClient = new SmtpClient("smtp.gmail.com"))
        {
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("mzarad905@gmail.com", "wjfl quzj sccw qgpr");
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            await smtpClient.SendMailAsync(mailMessage);
        }

        return Ok(response);
    }



    [Authorize]
    [HttpGet("mybookings")]
    public async Task<IActionResult> GetMyBookings()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
            return Unauthorized("Invalid token.");

        int userId = int.Parse(userIdClaim);

        var bookings = await _context.Bookings
            .Where(b => b.UserID == userId)
            .Include(b => b.Clinic)
            .Include(b => b.Doctor)
            .Include(b => b.User)
            .Select(b => new BookingDetailsDto
            {
                BookingID = b.BookingID, 
                UserName = b.User.Fname + " " + b.User.Lname,
                DoctorName = b.Doctor.FirstName + " " + b.Doctor.LastName,
                PhoneNumber = b.User.Phone,
                BookingPrice = b.BookingPrice,
                PaymentMethod = b.paymentMethod.ToString(),
                Day = b.Day,
                Time = b.Time,
                ClinicAddress = b.Clinic.Address
            })
            .ToListAsync();

        return Ok(bookings);
    }





    [Authorize]
    [HttpDelete("cancel-booking/{bookingId}")]
    public async Task<IActionResult> CancelBooking(int bookingId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized("Invalid token.");

        int userId = int.Parse(userIdClaim);

        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BookingID == bookingId && b.UserID == userId);

        if (booking == null)
            return NotFound("Booking not found or you do not have permission to cancel this booking.");

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Booking has been successfully cancelled." });
    }









}








