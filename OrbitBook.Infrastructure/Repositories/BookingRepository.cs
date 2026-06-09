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

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Destination)
                .Include(b => b.Status)
                .Include(b => b.User)
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

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}