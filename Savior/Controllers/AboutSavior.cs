using Microsoft.AspNetCore.Mvc;

namespace Savior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutSavior : ControllerBase
    {
        // GET: api/About
        [HttpGet]
        public IActionResult Get()
        {
            var aboutInfo = new
            {
                Title = "About Savior",
                Description = "Savior is a platform that provides users with emergency services such as doctors, pharmacies, and first aid support. It also connects users to emergency teams to help in critical situations.",
                Version = "1.0",
                Author = "Mohamed Zarad , Ahmed Sami , Norhan , Kholoud , Said Mohmoud  ",
                ContactEmail = "contact@saviorapp.com"
            };

            return Ok(aboutInfo);
        }
    }
}
