using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Services;

namespace OrbitBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationService _destinationService;

        public DestinationsController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        private bool IsAdmin() =>
            User.Claims.Any(c => c.Type == System.Security.Claims.ClaimTypes.Role && c.Value == "ADMIN");

        // GET /api/destinations — público
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var destinations = await _destinationService.GetAllDestinationsAsync();
            return Ok(destinations);
        }

        // GET /api/destinations/{id} — público
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var destination = await _destinationService.GetDestinationByIdAsync(id);
            if (destination == null)
                return NotFound(new { Message = "Destino não encontrado." });
            return Ok(destination);
        }

        // POST /api/destinations — somente ADMIN
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateDestinationDto dto)
        {
            if (!IsAdmin()) return Forbid();
            try
            {
                var created = await _destinationService.CreateDestinationAsync(dto);
                return Created(string.Empty, created);
            }
            catch (Exception ex) { return BadRequest(new { Message = ex.Message }); }
        }

        // PATCH /api/destinations/{id} — somente ADMIN
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDestinationDto dto)
        {
            if (!IsAdmin()) return Forbid();
            var updated = await _destinationService.UpdateDestinationAsync(id, dto);
            if (updated == null)
                return NotFound(new { Message = "Destino não encontrado." });
            return Ok(updated);
        }

        // DELETE /api/destinations/{id} — somente ADMIN
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return Forbid();
            var deleted = await _destinationService.DeleteDestinationAsync(id);
            if (!deleted)
                return NotFound(new { Message = "Destino não encontrado." });
            return Ok(new { Message = "Destino removido com sucesso." });
        }
    }
}