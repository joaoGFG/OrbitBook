using OrbitBook.Application.DTOs;

namespace OrbitBook.Application.Interfaces.Services
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationDto>> GetAllDestinationsAsync();
        Task<DestinationDto?> GetDestinationByIdAsync(int id);
        Task<DestinationDto> CreateDestinationAsync(CreateDestinationDto dto);
        Task<DestinationDto?> UpdateDestinationAsync(int id, UpdateDestinationDto dto);
        Task<bool> DeleteDestinationAsync(int id);
    }
}