using OrbitBook.Application.DTOs;

namespace OrbitBook.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetByDestinationIdAsync(int destinationId);
        Task<ReviewDto> CreateReviewAsync(int userId, CreateReviewDto dto);
        Task<bool> DeleteReviewAsync(int reviewId, int userId, bool isAdmin);
    }
}