namespace OrbitBook.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int PassengerId { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public string TicketClass { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string QrCode { get; set; } = string.Empty;

        public Booking? Booking { get; set; }
        public Passenger? Passenger { get; set; }
    }
}