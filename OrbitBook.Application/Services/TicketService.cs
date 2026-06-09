using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Application.Interfaces.Services;
using OrbitBook.Domain.Entities;

namespace OrbitBook.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IBookingRepository _bookingRepository;

        public TicketService(ITicketRepository ticketRepository, IBookingRepository bookingRepository)
        {
            _ticketRepository = ticketRepository;
            _bookingRepository = bookingRepository;
        }

        private static TicketDto ToDto(Ticket t) => new()
        {
            Id = t.Id,
            BookingId = t.BookingId,
            PassengerId = t.PassengerId,
            PassengerName = t.Passenger?.FullName ?? string.Empty,
            SeatNumber = t.SeatNumber,
            TicketClass = t.TicketClass,
            IssueDate = t.IssueDate,
            Status = t.Status,
            QrCode = t.QrCode
        };

        public async Task<IEnumerable<TicketDto>> GetTicketsByBookingIdAsync(int bookingId, int userId, bool isAdmin)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return Enumerable.Empty<TicketDto>();
            if (!isAdmin && booking.UserId != userId) return Enumerable.Empty<TicketDto>();

            var tickets = await _ticketRepository.GetByBookingIdAsync(bookingId);
            return tickets.Select(ToDto);
        }

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto dto)
        {
            var booking = await _bookingRepository.GetByIdAsync(dto.BookingId);
            if (booking == null)
                throw new InvalidOperationException("Reserva não encontrada.");

            // Gera um QR Code simples (em produção usar uma lib como QRCoder)
            var qrCode = $"ORBIT-{dto.BookingId}-{dto.PassengerId}-{Guid.NewGuid().ToString()[..8].ToUpper()}";

            var ticket = new Ticket
            {
                BookingId = dto.BookingId,
                PassengerId = dto.PassengerId,
                SeatNumber = dto.SeatNumber,
                TicketClass = dto.TicketClass,
                IssueDate = DateTime.UtcNow,
                Status = "ATIVO",
                QrCode = qrCode
            };

            var created = await _ticketRepository.AddAsync(ticket);
            return ToDto(created);
        }
    }
}