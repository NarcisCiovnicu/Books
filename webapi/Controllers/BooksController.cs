using BusinessLogic.Services;
using Domain.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(BookDTO book)
        {
            await _booksService.AddAsync(book);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _booksService.GetAllAsync();
            return Ok(books);
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _booksService.GetAsync(id);
            return Ok(book);
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update(BookDTO book)
        {
            await _booksService.UpdateAsync(book);
            return Ok();
        }

        [Route("Delete/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _booksService.DeleteAsync(id);
            return Ok();
        }
    }
}
