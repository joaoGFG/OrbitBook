using OrbitBook.Application.DTOs;

namespace OrbitBook.Application.Interfaces.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetTicketsByBookingIdAsync(int bookingId, int userId, bool isAdmin);
        Task<TicketDto> CreateTicketAsync(CreateTicketDto dto);
    }
}