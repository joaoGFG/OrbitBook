using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private static BookingDto ToDto(Booking b) => new()
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

        public async Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _bookingRepository.GetByUserIdAsync(userId);
            return bookings.Select(ToDto).ToList();
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return bookings.Select(ToDto).ToList();
        }

        public async Task<BookingDto?> GetBookingByIdAsync(int id, int userId)
        {
            var b = await _bookingRepository.GetByIdAsync(id);
            if (b == null || b.UserId != userId) return null;
            return ToDto(b);
        }

        public async Task<BookingDto?> CreateBookingAsync(int userId, CreateBookingDto dto)
        {
            var destination = await _destinationRepository.GetByIdAsync(dto.DestinationId);
            if (destination == null)
                throw new InvalidOperationException("Destino inválido.");

            // RN02: O número de passageiros de uma reserva não pode exceder a capacidade máxima/vagas do destino
            if (dto.NumPassengers > destination.AvailableSeats)
                throw new InvalidOperationException("Capacidade máxima ou vagas do destino excedida.");

            // RN07: Validação se a quantidade de passageiros detalhados no payload é igual ao informado no num_passengers
            if (dto.Passengers == null || dto.Passengers.Count != dto.NumPassengers)
                throw new InvalidOperationException("O número de passageiros detalhados deve ser exatamente igual à quantidade informada na reserva.");

            // Instancia a reserva alimentando a lista interna de passageiros (Navigation Property)
            var newBooking = new Booking
            {
                UserId = userId,
                DestinationId = dto.DestinationId,
                StatusId = 1, // PENDENTE
                DepartureDate = dto.DepartureDate,
                ReturnDate = dto.ReturnDate,
                NumPassengers = dto.NumPassengers,

                // RN03: O preço total da reserva é calculado automaticamente multiplicando o valor base pela quantidade
                TotalPrice = destination.BasePrice * dto.NumPassengers,

                // Mapeia a lista de passageiros recebida no DTO diretamente para a entidade filha
                Passengers = dto.Passengers.Select(p => new Passenger
                {
                    FullName = p.FullName,
                    DocumentNumber = p.DocumentNumber,
                    BirthDate = p.BirthDate,
                    MedicalStatus = p.MedicalStatus
                }).ToList()
            };

            // Ao salvar o Booking através do repositório, o EF Core detecta a lista interna de 
            // passageiros populada e gera o INSERT na tabela PASSENGERS em cascata automaticamente.
            await _bookingRepository.AddAsync(newBooking);

            return await GetBookingByIdAsync(newBooking.Id, userId);
        }

        public async Task<bool> UpdateStatusAsync(int bookingId, int userId, int statusId, bool isAdmin)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;
            if (!isAdmin && booking.UserId != userId) return false;

            booking.StatusId = statusId;
            await _bookingRepository.UpdateAsync(booking);
            return true;
        }

        public async Task<bool> DeleteAsync(int bookingId, int userId, bool isAdmin)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;
            if (!isAdmin && booking.UserId != userId) return false;

            await _bookingRepository.DeleteAsync(booking);
            return true;
        }
    }
}