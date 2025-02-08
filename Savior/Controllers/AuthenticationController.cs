using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savior.Data;
using Savior.Data;
using Savior.Models;
using Savior.Services;

namespace Savior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost("signup")]
        public async Task<ActionResult<User>> SignUp(User user)
        {
            if (user == null)
                return BadRequest("User data is required.");

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
                return Conflict("Email is already registered.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(SignUp), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            if (login == null)
                return BadRequest("Email and Password are required.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null || user.Password != login.Password)
                return Unauthorized("Invalid credentials.");

           
            return Ok("Login successful");
        }
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] string email, [FromServices] EmailService emailService)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return NotFound("Email not found.");

            
            var code = new Random().Next(100000, 999999).ToString();

           
            var subject = "Password Reset Code";
            var body = $"Your password reset code is: {code}";

            await emailService.SendEmailAsync(email, subject, body);

           
            user.ResetCode = code;
            user.ResetCodeExpiry = DateTime.UtcNow.AddMinutes(5);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("A code has been sent to your email.");
        }
        [HttpPost("confirm-code")]
        public async Task<ActionResult> ConfirmCode([FromBody] ConfirmCodeRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetCode == request.Code);

            if (user == null)
                return BadRequest("Invalid code.");

            if (user.ResetCodeExpiry < DateTime.UtcNow)
                return BadRequest("Code has expired.");

            return Ok("Code confirmed, you can now reset your password.");
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            if (model.NewPassword != model.ConfirmNewPassword)
                return BadRequest("New password and confirm password do not match.");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.ResetCode != null && u.ResetCodeExpiry > DateTime.UtcNow);

            if (user == null)
                return NotFound("Invalid or expired reset code.");

            user.Password = model.NewPassword;

            user.ResetCode = null;
            user.ResetCodeExpiry = null;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Password has been successfully reset.");
        }

    }
}
