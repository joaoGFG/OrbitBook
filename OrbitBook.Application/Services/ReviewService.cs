using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Application.Interfaces.Services;
using OrbitBook.Domain.Entities;

namespace OrbitBook.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookingRepository _bookingRepository;

        public ReviewService(IReviewRepository reviewRepository, IBookingRepository bookingRepository)
        {
            _reviewRepository = reviewRepository;
            _bookingRepository = bookingRepository;
        }

        private static ReviewDto ToDto(Review r) => new()
        {
            Id = r.Id,
            BookingId = r.BookingId,
            UserId = r.UserId,
            UserName = r.User?.Name ?? string.Empty,
            Rating = r.Rating,
            Comment = r.Comment
        };

        public async Task<IEnumerable<ReviewDto>> GetByDestinationIdAsync(int destinationId)
        {
            var reviews = await _reviewRepository.GetByDestinationIdAsync(destinationId);
            return reviews.Select(ToDto);
        }

        public async Task<ReviewDto> CreateReviewAsync(int userId, CreateReviewDto dto)
        {
            // Só pode avaliar reserva concluída (StatusId = 4 = CONCLUIDO)
            var booking = await _bookingRepository.GetByIdAsync(dto.BookingId);
            if (booking == null || booking.UserId != userId)
                throw new InvalidOperationException("Reserva não encontrada.");
            if (booking.StatusId != 4)
                throw new InvalidOperationException("Só é possível avaliar reservas concluídas.");

            var existing = await _reviewRepository.GetByBookingIdAsync(dto.BookingId);
            if (existing != null)
                throw new InvalidOperationException("Essa reserva já foi avaliada.");

            if (dto.Rating < 1 || dto.Rating > 5)
                throw new InvalidOperationException("A avaliação deve ser entre 1 e 5.");

            var review = new Review
            {
                BookingId = dto.BookingId,
                UserId = userId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            var created = await _reviewRepository.AddAsync(review);
            return ToDto(created);
        }

        public async Task<bool> DeleteReviewAsync(int reviewId, int userId, bool isAdmin)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null) return false;
            if (!isAdmin && review.UserId != userId) return false;

            await _reviewRepository.DeleteAsync(review);
            return true;
        }
    }
}