using Biosero.Api.Models;
using Biosero.Api.Utilities;
using Biosero.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Biosero.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly JwtHandler _jwtHandler;

        public AuthenticationController(JwtHandler jwtHandler, AuthenticationService authenticationService)
        {
            _jwtHandler = jwtHandler;
            _authenticationService = authenticationService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new AuthResponseDto { ErrorMessage = "User Name and Password are required" });
                }

                var result = await _authenticationService.Login(loginRequest.Username, loginRequest.Password);

                var token = _jwtHandler.GenerateToken(result); ;

                return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }
    }
}
