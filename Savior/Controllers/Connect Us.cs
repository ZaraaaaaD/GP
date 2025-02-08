using Microsoft.AspNetCore.Mvc;

namespace Savior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectUsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetConnectUsPage()
        {
            return Ok(new { message = "Welcome to the Connect Us Page!" });
        }
    }
}
