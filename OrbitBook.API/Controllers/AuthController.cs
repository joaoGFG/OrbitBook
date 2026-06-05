using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Services;

namespace OrbitBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            try
            {
                var message = await _authService.RegisterAsync(request);
                return Created(string.Empty, new { Message = message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.AuthenticateAsync(loginDto);
            if (result == null)
                return Unauthorized(new { Message = "Credenciais inválidas." });

            return Ok(result);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userEmail = User.Claims
                .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;

            return Ok(new { Message = "Você está autenticado!", Email = userEmail });
        }
    }
}