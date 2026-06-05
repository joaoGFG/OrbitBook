using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [AllowAnonymous] // Catálogo é público para visitantes
        public async Task<IActionResult> GetAll()
        {
            var destinations = await _destinationService.GetAllDestinationsAsync();
            return Ok(destinations);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var destination = await _destinationService.GetDestinationByIdAsync(id);
            if (destination == null) return NotFound(new { Message = "Destino não encontrado." });

            return Ok(destination);
        }
    }
}