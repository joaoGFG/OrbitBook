using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitBook.Domain.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public Role? Role { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}