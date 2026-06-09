using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitBook.Domain.Entities
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

        public Booking? Booking { get; set; }
        public User? User { get; set; }
    }
}