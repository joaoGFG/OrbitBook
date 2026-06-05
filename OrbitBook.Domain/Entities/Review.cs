namespace OrbitBook.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

        public Booking? Booking { get; set; }
        public User? User { get; set; }
    }
}