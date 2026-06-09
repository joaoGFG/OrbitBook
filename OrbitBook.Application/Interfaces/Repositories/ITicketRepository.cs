using OrbitBook.Domain.Entities;

namespace OrbitBook.Application.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetByBookingIdAsync(int bookingId);
        Task<Ticket?> GetByIdAsync(int id);
        Task<Ticket> AddAsync(Ticket ticket);
    }
}