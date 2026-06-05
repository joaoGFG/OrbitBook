using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Application.Services;
using OrbitBook.Domain.Entities;

namespace OrbitBook.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            // Arrange base (Configurações comuns)
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();

            // Isolando a configuração da chave JWT a ser lida pelo Serviço
            _configurationMock.Setup(c => c["JwtParameters:Secret"])
                              .Returns("OrbitBookSuperSecretKey2025ForJwtTokensNeedToBeLongEnoughToValidate");

            _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_ComCredenciaisValidas_DeveRetornarToken()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "carlos.souza@email.com", Password = "$2b$12$abc001hashed" };
            var mockUser = new User
            {
                Id = 1,
                Email = "carlos.souza@email.com",
                PasswordHash = "$2b$12$abc001hashed", // Simulando a representação exata do BD que me passou
                Role = new Role { Id = 1, Name = "VIAJANTE" }
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(loginDto.Email))
                               .ReturnsAsync(mockUser);

            // Act
            var result = await _authService.AuthenticateAsync(loginDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Token);
            Assert.Equal(1, result.UserId);
            Assert.Equal("VIAJANTE", result.Role);
        }

        [Fact]
        public async Task AuthenticateAsync_ComEmailInvalido_DeveRetornarNulo()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "email_inexistente@email.com", Password = "senha" };
            
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(loginDto.Email))
                               .ReturnsAsync((User?)null); // Não achou ninguém com o email

            // Act
            var result = await _authService.AuthenticateAsync(loginDto);

            // Assert
            Assert.Null(result); // A premissa é que se não logar, o token seja nulo
        }

        [Fact]
        public async Task AuthenticateAsync_ComSenhaInvalida_DeveRetornarNulo()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "carlos.souza@email.com", Password = "senhaIncorreta" };
            var mockUser = new User 
            { 
                Id = 1, 
                Email = "carlos.souza@email.com", 
                PasswordHash = "$2b$12$abc001hashed" // Senha do DB 
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(loginDto.Email))
                               .ReturnsAsync(mockUser);

            // Act
            var result = await _authService.AuthenticateAsync(loginDto);

            // Assert
            Assert.Null(result);
        }
    }
}