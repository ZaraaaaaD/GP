using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savior.Data;
using Savior.Models;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
[ApiController]
[Route("api/emergency")]
public class EmergencyController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmergencyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("request-emergency")]
    public async Task<IActionResult> RequestEmergency([FromBody] Emergency emergencyRequest)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized(new { message = "User is not authorized." });

        emergencyRequest.UserID = int.Parse(userId);
        emergencyRequest.RequestTime = DateTime.UtcNow;

        await _context.Emergencies.AddAsync(emergencyRequest);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Emergency request submitted successfully!", emergency = emergencyRequest });
    }

    [HttpPost("request-medical")]
    public async Task<IActionResult> RequestMedical([FromBody] MedicalTeam medicalRequest)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized(new { message = "User is not authorized." });

        medicalRequest.UserID = int.Parse(userId);
        medicalRequest.RequestTime = DateTime.UtcNow;

        await _context.MedicalTeams.AddAsync(medicalRequest);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Medical team request submitted successfully!", medicalTeam = medicalRequest });
    }

    [HttpGet("my-requests")]
    public async Task<IActionResult> GetUserRequests()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized(new { message = "User is not authorized." });

        var emergencyRequests = await _context.Emergencies
            .Where(e => e.UserID == int.Parse(userId))
            .Select(e => new
            {
                e.EmergencyID,
                e.Type,
                e.Location,
                e.RequestTime
            }).ToListAsync();

        var medicalRequests = await _context.MedicalTeams
            .Where(m => m.UserID == int.Parse(userId))
            .Select(m => new
            {
                m.TeamID,
                m.Role,
                m.Location,
                m.Phone,
                m.RequestTime
            }).ToListAsync();

        return Ok(new
        {
            emergencyRequests,
            medicalRequests
        });
    }

    [HttpPost("cancel-emergency")]
    public async Task<IActionResult> CancelEmergency([FromBody] int emergencyId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized(new { message = "User is not authorized." });

        var emergency = await _context.Emergencies
            .FirstOrDefaultAsync(e => e.EmergencyID == emergencyId && e.UserID == int.Parse(userId));

        if (emergency == null)
            return NotFound(new { message = "Emergency request not found or already canceled." });

        _context.Emergencies.Remove(emergency);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Emergency request has been canceled successfully." });
    }

    [HttpPost("cancel-medical")]
    public async Task<IActionResult> CancelMedical([FromBody] int teamId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized(new { message = "User is not authorized." });

        var medicalRequest = await _context.MedicalTeams
            .FirstOrDefaultAsync(m => m.TeamID == teamId && m.UserID == int.Parse(userId));

        if (medicalRequest == null)
            return NotFound(new { message = "Medical team request not found or already canceled." });

        _context.MedicalTeams.Remove(medicalRequest);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Medical team request has been canceled successfully." });
    }
}
