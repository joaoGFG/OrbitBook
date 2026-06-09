using Moq;
using OrbitBook.Application.DTOs;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Application.Services;
using OrbitBook.Domain.Entities;
using Microsoft.Extensions.Configuration;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(c => c["JwtParameters:Secret"])
                          .Returns((string?)"OrbitBookSuperSecretKey2025ForJwtTokensNeedToBeLongEnoughToValidate");
        _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task AuthenticateAsync_ComCredenciaisValidas_DeveRetornarToken()
    {
        // Arrange — gera um hash real para o teste
        var senhaReal = "senha123";
        var hashReal = BCrypt.Net.BCrypt.HashPassword(senhaReal);

        var loginDto = new LoginDto { Email = "carlos.souza@email.com", Password = senhaReal };
        var mockUser = new User
        {
            Id = 1,
            Email = "carlos.souza@email.com",
            PasswordHash = hashReal, // hash gerado pelo BCrypt
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
                           .ReturnsAsync((User?)null);
        // Act
        var result = await _authService.AuthenticateAsync(loginDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AuthenticateAsync_ComSenhaInvalida_DeveRetornarNulo()
    {
        // Arrange
        var senhaCorreta = "senha123";
        var hashReal = BCrypt.Net.BCrypt.HashPassword(senhaCorreta);

        var loginDto = new LoginDto { Email = "carlos.souza@email.com", Password = "senhaErrada" };
        var mockUser = new User
        {
            Id = 1,
            Email = "carlos.souza@email.com",
            PasswordHash = hashReal
        };

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(loginDto.Email))
                           .ReturnsAsync(mockUser);
        // Act
        var result = await _authService.AuthenticateAsync(loginDto);

        // Assert
        Assert.Null(result);
    }
}