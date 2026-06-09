using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Application.Interfaces.Services;
using OrbitBook.Domain.Entities;

namespace OrbitBook.Application.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _repository;

        public DestinationService(IDestinationRepository repository)
        {
            _repository = repository;
        }

        private static DestinationDto ToDto(Destination d) => new()
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

        public async Task<IEnumerable<DestinationDto>> GetAllDestinationsAsync()
        {
            var destinations = await _repository.GetAllAsync();
            return destinations.Select(ToDto);
        }

        public async Task<DestinationDto?> GetDestinationByIdAsync(int id)
        {
            var d = await _repository.GetByIdAsync(id);
            return d == null ? null : ToDto(d);
        }

        public async Task<DestinationDto> CreateDestinationAsync(CreateDestinationDto dto)
        {
            var destination = new Destination
            {
                TypeId = dto.TypeId,
                Name = dto.Name,
                Description = dto.Description,
                DistanceKm = dto.DistanceKm,
                BasePrice = dto.BasePrice,
                Capacity = dto.Capacity,
                AvailableSeats = dto.AvailableSeats,
                ImageUrl = dto.ImageUrl
            };

            var created = await _repository.AddAsync(destination);
            return ToDto(created);
        }

        public async Task<DestinationDto?> UpdateDestinationAsync(int id, UpdateDestinationDto dto)
        {
            var destination = await _repository.GetByIdAsync(id);
            if (destination == null) return null;

            if (dto.Name != null) destination.Name = dto.Name;
            if (dto.Description != null) destination.Description = dto.Description;
            if (dto.BasePrice.HasValue) destination.BasePrice = dto.BasePrice.Value;
            if (dto.AvailableSeats.HasValue) destination.AvailableSeats = dto.AvailableSeats.Value;
            if (dto.ImageUrl != null) destination.ImageUrl = dto.ImageUrl;

            await _repository.UpdateAsync(destination);
            return ToDto(destination);
        }

        public async Task<bool> DeleteDestinationAsync(int id)
        {
            var destination = await _repository.GetByIdAsync(id);
            if (destination == null) return false;
            await _repository.DeleteAsync(destination);
            return true;
        }
    }
}