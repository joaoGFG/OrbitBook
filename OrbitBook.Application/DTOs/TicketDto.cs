namespace OrbitBook.Application.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int PassengerId { get; set; }
        public string PassengerName { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public string TicketClass { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string QrCode { get; set; } = string.Empty;
    }

    public class CreateTicketDto
    {
        public int BookingId { get; set; }
        public int PassengerId { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public string TicketClass { get; set; } = string.Empty;
    }
}