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
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
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

        // GET /api/tickets/booking/{bookingId} — tickets de uma reserva
        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetByBooking(int bookingId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var tickets = await _ticketService.GetTicketsByBookingIdAsync(bookingId, userId, IsAdmin());
                return Ok(tickets);
            }
            catch (Exception ex) { return BadRequest(new { Message = ex.Message }); }
        }

        // POST /api/tickets — emitir ticket para um passageiro
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto dto)
        {
            try
            {
                var ticket = await _ticketService.CreateTicketAsync(dto);
                return Created(string.Empty, ticket);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}