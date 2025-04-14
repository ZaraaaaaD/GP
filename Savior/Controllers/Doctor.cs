using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savior.Data;
using Savior.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/doctor")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DoctorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. Submit registration request
    [HttpPost("register")]
    public async Task<IActionResult> RegisterDoctor([FromBody] Doctor doctor)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data.");

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return Ok("Your registration request has been received and is under review.");
    }

    // 2. Get all registered doctors
    [HttpGet("doctors")]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
    {
        return await _context.Doctors.ToListAsync();
    }

    // 3. Get doctor by ID
    [HttpGet("doctor/{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
            return NotFound("Doctor not found.");

        return doctor;
    }

    // 4. Update doctor info
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor doctor)
    {
        if (id != doctor.DoctorID)
            return BadRequest("Doctor ID mismatch.");

        _context.Entry(doctor).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok("Doctor information updated successfully.");
    }

    // 5. Delete a doctor (if needed)
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
            return NotFound("Doctor not found.");

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();

        return Ok("Doctor deleted successfully.");
    }

    // 6. Book an appointment
    [HttpPost("book")]
    public async Task<IActionResult> BookAppointment([FromBody] Booking booking)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid booking data.");

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return Ok("Appointment booked successfully.");
    }

    // 7. Get user bookings
    [HttpGet("bookings/{userId}")]
    public async Task<ActionResult<IEnumerable<Booking>>> GetUserBookings(int userId)
    {
        var bookings = await _context.Bookings.Where(b => b.UserID == userId).ToListAsync();
        return bookings;
    }

    // 8. Cancel booking
    [HttpDelete("cancel/{bookingId}")]
    public async Task<IActionResult> CancelBooking(int bookingId)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);
        if (booking == null)
            return NotFound("Booking not found.");

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return Ok("Booking cancelled successfully.");
    }
}
