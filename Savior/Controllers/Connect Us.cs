using Microsoft.AspNetCore.Mvc;
using Savior.Data;
using Savior.Models;
using System.Linq;
using System.Threading.Tasks;

[Route("api/connectus")]
[ApiController]
public class ConnectUsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ConnectUsController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> SubmitFeedback([FromBody] ContactUs model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.ContactUs.Add(model);
        await _context.SaveChangesAsync();

        return Ok("Feedback submitted successfully");
    }

    [HttpGet]
    public IActionResult GetFeedback()
    {
        var feedbackList = _context.ContactUs
            .Select(f => new { f.Name,  f.Feedback })
            .ToList();

        return Ok(feedbackList);
    }
}
