using Microsoft.AspNetCore.Mvc;

namespace Savior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDoctorPage()
        {
            return Ok(new { message = "Welcome to the Doctor Page!" });
        }
    }
}
