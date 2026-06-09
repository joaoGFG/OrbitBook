using Microsoft.EntityFrameworkCore;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Domain.Entities;
using OrbitBook.Infrastructure.Data;

namespace OrbitBook.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly OrbitBookDbContext _context;

        public TicketRepository(OrbitBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Tickets
                .Include(t => t.Passenger)
                .Where(t => t.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Passenger)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }
    }
}