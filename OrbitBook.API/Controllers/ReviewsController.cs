using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Services;
using System.Security.Claims;

namespace OrbitBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
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

        // GET /api/reviews/destination/{id} — público
        [HttpGet("destination/{destinationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByDestination(int destinationId)
        {
            var reviews = await _reviewService.GetByDestinationIdAsync(destinationId);
            return Ok(reviews);
        }

        // POST /api/reviews — requer missão CONCLUIDA
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var review = await _reviewService.CreateReviewAsync(userId, dto);
                return Created(string.Empty, review);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE /api/reviews/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var deleted = await _reviewService.DeleteReviewAsync(id, userId, IsAdmin());
                if (!deleted)
                    return NotFound(new { Message = "Avaliação não encontrada ou sem permissão." });
                return Ok(new { Message = "Avaliação removida com sucesso." });
            }
            catch (Exception ex) { return BadRequest(new { Message = ex.Message }); }
        }
    }
}