using Microsoft.AspNetCore.Mvc;

namespace Savior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPharmacyPage()
        {
            return Ok(new { message = "Welcome to the Pharmacy Page!" });
        }
    }
}
