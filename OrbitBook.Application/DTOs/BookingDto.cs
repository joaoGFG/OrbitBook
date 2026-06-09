using System;
using System.Collections.Generic;

namespace OrbitBook.Application.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DestinationId { get; set; }
        public string DestinationName { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int NumPassengers { get; set; }
    }

    public class CreateBookingDto
    {
        public int DestinationId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int NumPassengers { get; set; }

        public List<CreateBookingPassengerDto> Passengers { get; set; } = new();
    }

    public class CreateBookingPassengerDto
    {
        public string FullName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string MedicalStatus { get; set; } = string.Empty;
    }

    public class UpdateBookingStatusDto
    {
        public int StatusId { get; set; }
    }
}