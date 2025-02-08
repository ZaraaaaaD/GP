using Microsoft.AspNetCore.Mvc;

namespace Savior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHomePage()
        {
            return Ok(new { message = "Welcome to the Home Page!" });
        }
    }
}
