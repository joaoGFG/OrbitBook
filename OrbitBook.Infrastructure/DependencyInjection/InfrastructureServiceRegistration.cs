using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrbitBook.Application.Interfaces.Repositories;
using OrbitBook.Application.Interfaces.Services;
using OrbitBook.Application.Services;
using OrbitBook.Infrastructure.Data;
using OrbitBook.Infrastructure.Repositories;

namespace OrbitBook.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            // Opcional: Adicionar a string de conexão na API
            services.AddDbContext<OrbitBookDbContext>(options =>
                options.UseOracle(connectionString));

            // Injeção dos Repositórios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDestinationRepository, DestinationRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            
            // Injeção dos Services da Application
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDestinationService, DestinationService>();
            services.AddScoped<IBookingService, BookingService>();

            return services;
        }
    }
}