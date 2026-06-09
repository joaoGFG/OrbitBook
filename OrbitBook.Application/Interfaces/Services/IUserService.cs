using OrbitBook.Application.DTOs;

namespace OrbitBook.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<bool> DeleteUserAsync(int id);
    }
}