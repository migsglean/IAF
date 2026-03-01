using IAF.Server.Domain.DTO;
using IAF.Server.Exceptions;
using IAF.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IAF.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto request)
        {
            var response = await _authService.SignUpAsync(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
