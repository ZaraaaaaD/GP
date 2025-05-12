using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savior.Data; 
using System.Linq;
using Savior.Dto;
using System.Threading.Tasks;

namespace Savior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("PendingDoctors")]
        public async Task<IActionResult> GetPendingDoctors()
        {
            var pendingDoctors = await _context.Doctors
                .Where(d => d.IsApproved == false)
                .ToListAsync();

            return Ok(pendingDoctors);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("ApproveDoctor/{id}")]
        public async Task<IActionResult> ApproveDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound("Doctor not found");

            doctor.IsApproved = true;
            await _context.SaveChangesAsync();

            return Ok("Doctor approved successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RejectDoctor/{id}")]
        public async Task<IActionResult> RejectDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound("Doctor not found");

            _context.Doctors.Remove(doctor); 
            await _context.SaveChangesAsync();

            return Ok("Doctor rejected and removed successfully");
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet("admin/all-bookings")]
        //public async Task<IActionResult> GetAllBookingsWithDetails()
        //{
        //    try
        //    {
        //        var bookings = await _context.Bookings
        //            .Include(b => b.User)
        //            .Include(b => b.Doctor)
        //            .Include(b => b.Clinic)
        //            .ToListAsync();

        //        var bookingDetails = bookings.Select(b => new BookingDetailsDto
        //        {
        //            BookingID = b.BookingID,
        //            UserName = b.User != null ? $"{b.User.Fname} {b.User.Lname}" : "Unknown",
        //            DoctorName = b.Doctor != null ? $"{b.Doctor.FirstName} {b.Doctor.LastName}" : "Unknown",
        //            PhoneNumber = b.User?.Phone ?? "N/A",
        //            BookingPrice = b.BookingPrice,
        //            PaymentMethod = b.paymentMethod.ToString(),
        //            Day = b.Day ?? "N/A",
        //            Time = b.Time ?? "N/A",
        //            ClinicAddress = b.Clinic?.Address ?? "N/A"
        //        }).ToList();

        //        return Ok(bookingDetails);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
        //    }
        //}
        [Authorize(Roles = "Admin")]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _context.Users
                    .Select(u => new
                    {
                        u.Id,
                        u.Fname,
                        u.Lname,
                        u.Email,
                        u.Phone
                    })
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
