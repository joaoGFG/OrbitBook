using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Application.Interfaces.Services;

namespace OrbitBook.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                DocumentNumber = u.DocumentNumber,
                Role = u.Role?.Name ?? string.Empty
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var u = await _userRepository.GetByIdAsync(id);
            if (u == null) return null;
            return new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                DocumentNumber = u.DocumentNumber,
                Role = u.Role?.Name ?? string.Empty
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;
            await _userRepository.DeleteAsync(user);
            return true;
        }
    }
}