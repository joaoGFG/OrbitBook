using OrbitBook.Application.DTOs;

namespace OrbitBook.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId);
        Task<BookingDto?> GetBookingByIdAsync(int id, int userId);
        Task<BookingDto?> CreateBookingAsync(int userId, CreateBookingDto dto);
    }
}