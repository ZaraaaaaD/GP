using Microsoft.AspNetCore.Mvc;

namespace Savior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyTeamController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetEmergencyTeamPage()
        {
            return Ok(new { message = "Welcome to the Emergency Team Page!" });
        }
    }
}
