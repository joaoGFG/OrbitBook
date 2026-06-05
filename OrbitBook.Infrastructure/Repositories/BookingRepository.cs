using Microsoft.EntityFrameworkCore;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Domain.Entities;
using OrbitBook.Infrastructure.Data;

namespace OrbitBook.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly OrbitBookDbContext _context;

        public BookingRepository(OrbitBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetByUserIdAsync(int userId)
        {
            return await _context.Bookings
                .Include(b => b.Destination)
                .Include(b => b.Status)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Destination)
                .Include(b => b.Status)
                .Include(b => b.Passengers)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
    }
}