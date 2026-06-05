using OrbitBook.Application.DTOs;

namespace OrbitBook.Application.Interfaces.Services
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationDto>> GetAllDestinationsAsync();
        Task<DestinationDto?> GetDestinationByIdAsync(int id);
    }
}