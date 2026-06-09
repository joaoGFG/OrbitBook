using OrbitBook.Domain.Entities;

namespace OrbitBook.Application.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetByDestinationIdAsync(int destinationId);
        Task<Review?> GetByIdAsync(int id);
        Task<Review?> GetByBookingIdAsync(int bookingId);
        Task<Review> AddAsync(Review review);
        Task DeleteAsync(Review review);
    }
}