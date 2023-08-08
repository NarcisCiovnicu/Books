using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorsService.GetAllAsync();
            return Ok(authors);
        }
    }
}
