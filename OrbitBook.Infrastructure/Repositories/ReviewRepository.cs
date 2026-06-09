using Microsoft.EntityFrameworkCore;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Domain.Entities;
using OrbitBook.Infrastructure.Data;

namespace OrbitBook.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly OrbitBookDbContext _context;

        public ReviewRepository(OrbitBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetByDestinationIdAsync(int destinationId)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Booking)
                .Where(r => r.Booking != null && r.Booking.DestinationId == destinationId)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Review?> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.BookingId == bookingId);
        }

        public async Task<Review> AddAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task DeleteAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}