using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interface;

namespace PRN_ASSI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountServices _authService;
        public AuthController(IAccountServices authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest dto)
        {
            var response = await _authService.Login(dto);
            if(response == null)
            {
                return Unauthorized("Invalid Email or Password");
            }
            return Ok(response);
        }
        [Authorize(Roles = "1, 2")]
        [HttpGet("test")]
        public async Task<IActionResult> HelloWord()
        {
            return Ok("Completed to authorize");
        }
    }
}
