namespace OrbitBook.Domain.Entities
{
    public class AiRecommendation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? DestinationId { get; set; }
        public string PromptUsed { get; set; } = string.Empty;
        public string ResponseText { get; set; } = string.Empty;
        public string ModelUsed { get; set; } = string.Empty;

        public User? User { get; set; }
        public Destination? Destination { get; set; }
    }
}