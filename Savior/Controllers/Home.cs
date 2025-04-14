using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savior.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Savior.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Home/MyOrders
        [HttpGet("MyOrders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var emergencyOrders = await _context.Emergencies
                .Where(e => e.UserID == userId)
                .ToListAsync();

            var medicalTeamOrders = await _context.MedicalTeams
                .Where(m => m.UserID == userId)
                .ToListAsync();

            return Ok(new
            {
                EmergencyOrders = emergencyOrders,
                MedicalTeamOrders = medicalTeamOrders
            });
        }

        // GET: api/Home/MyRegistration
        [HttpGet("MyRegistration")]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var bookings = await _context.Bookings
                .Where(b => b.UserID == userId)
                .Include(b => b.Clinic)
                .ToListAsync();

            return Ok(bookings);
        }
    }
}
