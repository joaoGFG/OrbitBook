using OrbitBook.Domain.Entities;

namespace OrbitBook.Application.Interfaces.Repositories
{
    public interface IDestinationRepository
    {
        Task<IEnumerable<Destination>> GetAllAsync();
        Task<Destination?> GetByIdAsync(int id);
        Task<Destination> AddAsync(Destination destination);
        Task UpdateAsync(Destination destination);
        Task DeleteAsync(Destination destination);
    }
}