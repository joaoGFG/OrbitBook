namespace OrbitBook.Domain.Entities
{
    public class Destination
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long DistanceKm { get; set; }
        public decimal BasePrice { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        // Propriedades de navegação
        public DestinationType? Type { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}