namespace OrbitBook.Application.DTOs
{
    public class TokenResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}