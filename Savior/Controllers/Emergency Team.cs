using Microsoft.AspNetCore.Mvc;

namespace Savior.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmergencyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Welcome()
        {


            return Ok("Welcome to Emergency!");



        }
    }
}
