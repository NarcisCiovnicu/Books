using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("/")]
    public class RootController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("API is up and running");
        }
    }
}
