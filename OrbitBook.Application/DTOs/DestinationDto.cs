using OrbitBook.Domain.Entities;

namespace OrbitBook.Application.DTOs
{
    public class DestinationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long DistanceKm { get; set; }
        public decimal BasePrice { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
        public string? ImageUrl { get; set; }
        public string TypeName { get; set; } = string.Empty;
    }

    public class CreateDestinationDto
    {
        public int TypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long DistanceKm { get; set; }
        public decimal BasePrice { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class UpdateDestinationDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? BasePrice { get; set; }
        public int? AvailableSeats { get; set; }
        public string? ImageUrl { get; set; }
    }
}