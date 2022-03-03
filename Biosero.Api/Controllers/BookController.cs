using Biosero.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Biosero.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {


            return Ok();
        }

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] SearchRequest searchRequest)
        {
           
            return Ok();
        }
    }
}
