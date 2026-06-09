using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrbitBook.Application.Interfaces.Services;
using System.Security.Claims;

namespace OrbitBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        private int GetCurrentUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
                return userId;
            throw new UnauthorizedAccessException("Usuário inválido no token.");
        }

        private bool IsAdmin() =>
            User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "ADMIN");

        // GET /api/users — somente ADMIN
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!IsAdmin()) return Forbid();
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET /api/users/{id} — próprio usuário ou ADMIN
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                if (!IsAdmin() && currentUserId != id)
                    return Forbid();

                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                    return NotFound(new { Message = "Usuário não encontrado." });
                return Ok(user);
            }
            catch (Exception ex) { return BadRequest(new { Message = ex.Message }); }
        }

        // DELETE /api/users/{id} — somente ADMIN
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!IsAdmin()) return Forbid();
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound(new { Message = "Usuário não encontrado." });
            return Ok(new { Message = "Usuário removido com sucesso." });
        }
    }
}