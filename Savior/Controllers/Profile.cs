using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Savior.Data;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]  
[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProfileController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize] 
    [HttpGet]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        if (userId == null)
        {
            return Unauthorized();
        }

        var user = _context.Users.Find(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(new
        {
            Fname = user.Fname,
            Lname = user.Lname,
            Email = user.Email,
            Phone = user.Phone
        });
    }


    [HttpPut("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateData model)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized("User not authenticated");
        }

        var user = await _context.Users.FindAsync(int.Parse(userId));


        if (user == null)
        {
            return NotFound("User not found");
        }

        user.Fname = model.Fname;
        user.Lname = model.Lname;
        user.Email = model.Email;
        user.Phone = model.Phone;

        await _context.SaveChangesAsync();

        return Ok("Profile updated successfully");
    }

    [HttpPut("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePassword model)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized("User not authenticated");
        }

        var user = await _context.Users.FindAsync(int.Parse(userId));

        if (user == null)
        {
            return NotFound("User not found");
        }

        if (user.Password != model.OldPassword)
        {
            return BadRequest("Old password is incorrect");
        }

        if (model.NewPassword != model.ConfirmNewPassword)
        {
            return BadRequest("New passwords do not match");
        }

        user.Password = model.NewPassword;
        await _context.SaveChangesAsync();

        return Ok("Password updated successfully");
    }
}
