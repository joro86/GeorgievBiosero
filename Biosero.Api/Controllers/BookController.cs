using Biosero.Api.Models;
using Biosero.Service.Models;
using Biosero.Service.Models.Api;
using Biosero.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Biosero.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _bookService.GetBookDetail(id);

            return Ok(result);
        }

        /// <summary>
        /// User Query string parameter to serach for Title, Description, Author...
        /// Example /api/book/list?title=book 1
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] BookSearchRequest searchRequest)
        {
            var result = await _bookService.GetFilteredBookList(searchRequest);

            return Ok(result);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] BookRequest book)
        {
            
            var result = await _bookService.CreateBook(book);

            return Ok(result);
        }

        [HttpPatch("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] BookDto book)
        {
            var result = await _bookService.Update(book);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            //Make sure you are passing JWT token to this request in order to properly register IUserContext and get the correct user ID
            await _bookService.Delete(id);

            return Ok();
        }
    }
}
