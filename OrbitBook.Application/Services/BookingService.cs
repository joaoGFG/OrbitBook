using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Application.Interfaces.Services;
using OrbitBook.Domain.Entities;

namespace OrbitBook.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IDestinationRepository _destinationRepository;

        public BookingService(IBookingRepository bookingRepository, IDestinationRepository destinationRepository)
        {
            _bookingRepository = bookingRepository;
            _destinationRepository = destinationRepository;
        }

        public async Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _bookingRepository.GetByUserIdAsync(userId);
            return bookings.Select(b => new BookingDto
            {
                Id = b.Id,
                UserId = b.UserId,
                DestinationId = b.DestinationId,
                DestinationName = b.Destination?.Name ?? string.Empty,
                StatusId = b.StatusId,
                StatusName = b.Status?.Name ?? string.Empty,
                DepartureDate = b.DepartureDate,
                ReturnDate = b.ReturnDate,
                TotalPrice = b.TotalPrice,
                NumPassengers = b.NumPassengers
            });
        }

        public async Task<BookingDto?> GetBookingByIdAsync(int id, int userId)
        {
            var b = await _bookingRepository.GetByIdAsync(id);
            if (b == null || b.UserId != userId) return null;

            return new BookingDto
            {
                Id = b.Id,
                UserId = b.UserId,
                DestinationId = b.DestinationId,
                DestinationName = b.Destination?.Name ?? string.Empty,
                StatusId = b.StatusId,
                StatusName = b.Status?.Name ?? string.Empty,
                DepartureDate = b.DepartureDate,
                ReturnDate = b.ReturnDate,
                TotalPrice = b.TotalPrice,
                NumPassengers = b.NumPassengers
            };
        }

        public async Task<BookingDto?> CreateBookingAsync(int userId, CreateBookingDto dto)
        {
            // Validações básicas e regra de negócio
            var destination = await _destinationRepository.GetByIdAsync(dto.DestinationId);
            if (destination == null) throw new Exception("Destino inválido.");

            if (dto.NumPassengers > destination.AvailableSeats)
                throw new Exception("Capacidade máxima do destino excedida.");

            var totalPrice = destination.BasePrice * dto.NumPassengers;

            var newBooking = new Booking
            {
                UserId = userId,
                DestinationId = dto.DestinationId,
                StatusId = 1, // 1 = PENDENTE
                DepartureDate = dto.DepartureDate,
                ReturnDate = dto.ReturnDate,
                NumPassengers = dto.NumPassengers,
                TotalPrice = totalPrice
            };

            await _bookingRepository.AddAsync(newBooking);

            // Buscar dnv pra carregar nav properties
            return await GetBookingByIdAsync(newBooking.Id, userId);
        }
    }
}