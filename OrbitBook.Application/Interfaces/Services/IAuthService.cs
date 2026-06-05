using OrbitBook.Application.DTOs;

namespace OrbitBook.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> AuthenticateAsync(LoginDto loginDto);
        Task<string> RegisterAsync(RegisterDto request);
    }
}