using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Services;
using System.Security.Claims;

namespace OrbitBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Só usuários autenticados podem interagir com as reservas
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        private int GetCurrentUserId()
        {
            // Extrai o ID do usuário diretamente do Payload do token JWT
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("Usuário inválido no token.");
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBookings()
        {
            try
            {
                var userId = GetCurrentUserId();
                var bookings = await _bookingService.GetUserBookingsAsync(userId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var booking = await _bookingService.GetBookingByIdAsync(id, userId);

                if (booking == null) return NotFound(new { Message = "Reserva não encontrada ou pertence a outro usuário." });

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var newBooking = await _bookingService.CreateBookingAsync(userId, dto);
                
                return CreatedAtAction(nameof(GetBookingById), new { id = newBooking?.Id }, newBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}