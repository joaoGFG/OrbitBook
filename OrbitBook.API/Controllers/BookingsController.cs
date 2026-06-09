using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Services;
using System.Security.Claims;

namespace OrbitBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
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

        // GET /api/bookings — minhas reservas
        [HttpGet]
        public async Task<IActionResult> GetMyBookings()
        {
            try
            {
                var userId = GetCurrentUserId();
                var bookings = await _bookingService.GetUserBookingsAsync(userId);
                return Ok(bookings);
            }
            catch (Exception ex) { return BadRequest(new { Message = ex.Message }); }
        }

        // GET /api/bookings/all — todas as reservas (ADMIN)
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookings()
        {
            if (!IsAdmin()) return Forbid();
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // GET /api/bookings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var booking = await _bookingService.GetBookingByIdAsync(id, userId);
                if (booking == null)
                    return NotFound(new { Message = "Reserva não encontrada ou pertence a outro usuário." });
                return Ok(booking);
            }
            catch (Exception ex) { return BadRequest(new { Message = ex.Message }); }
        }

        // POST /api/bookings
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var newBooking = await _bookingService.CreateBookingAsync(userId, dto);
                return CreatedAtAction(nameof(GetBookingById), new { id = newBooking?.Id }, newBooking);
            }
            catch (Exception ex) { return BadRequest(new { Message = ex.Message }); }
        }

        // PATCH /api/bookings/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateBookingStatusDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var updated = await _bookingService.UpdateStatusAsync(id, userId, dto.StatusId, IsAdmin());
                if (!updated)
                    return NotFound(new { Message = "Reserva não encontrada ou sem permissão." });
                return Ok(new { Message = "Status atualizado com sucesso." });
            }
            catch (Exception ex) { return BadRequest(new { Message = ex.Message }); }
        }

        // DELETE /api/bookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var deleted = await _bookingService.DeleteAsync(id, userId, IsAdmin());
                if (!deleted)
                    return NotFound(new { Message = "Reserva não encontrada ou sem permissão." });
                return Ok(new { Message = "Reserva cancelada com sucesso." });
            }
            catch (Exception ex) { return BadRequest(new { Message = ex.Message }); }
        }
    }
}