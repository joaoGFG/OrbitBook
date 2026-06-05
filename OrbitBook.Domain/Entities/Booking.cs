namespace OrbitBook.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DestinationId { get; set; }
        public int StatusId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int NumPassengers { get; set; }

        // Propriedades de navegação
        public User? User { get; set; }
        public Destination? Destination { get; set; }
        public BookingStatus? Status { get; set; }
        
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
        public Payment? Payment { get; set; }
        public Review? Review { get; set; }
    }
}