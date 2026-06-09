using OrbitBook.Domain.Entities;

namespace OrbitBook.Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking> AddAsync(Booking booking);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Booking booking);
    }
}