namespace OrbitBook.Domain.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string MedicalStatus { get; set; } = string.Empty;

        public Booking? Booking { get; set; }
    }
}