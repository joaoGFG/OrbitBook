using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Application.Interfaces.Services;

namespace OrbitBook.Application.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _repository;

        public DestinationService(IDestinationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DestinationDto>> GetAllDestinationsAsync()
        {
            var destinations = await _repository.GetAllAsync();
            return destinations.Select(d => new DestinationDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                DistanceKm = d.DistanceKm,
                BasePrice = d.BasePrice,
                Capacity = d.Capacity,
                AvailableSeats = d.AvailableSeats,
                ImageUrl = d.ImageUrl,
                TypeName = d.Type?.Name ?? string.Empty
            });
        }

        public async Task<DestinationDto?> GetDestinationByIdAsync(int id)
        {
            var d = await _repository.GetByIdAsync(id);
            if (d == null) return null;

            return new DestinationDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                DistanceKm = d.DistanceKm,
                BasePrice = d.BasePrice,
                Capacity = d.Capacity,
                AvailableSeats = d.AvailableSeats,
                ImageUrl = d.ImageUrl,
                TypeName = d.Type?.Name ?? string.Empty
            };
        }
    }
}