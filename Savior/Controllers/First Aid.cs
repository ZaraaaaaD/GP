using Microsoft.AspNetCore.Mvc;

namespace Savior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstAidController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFirstAidPage()
        {
            return Ok(new { message = "Welcome to the First Aid Page!" });
        }
    }
}
