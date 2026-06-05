using Microsoft.EntityFrameworkCore;
using OrbitBook.Domain.Entities;

namespace OrbitBook.Infrastructure.Data
{
    public class OrbitBookDbContext : DbContext
    {
        public OrbitBookDbContext(DbContextOptions<OrbitBookDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DestinationType> DestinationTypes { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AiRecommendation> AiRecommendations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeamento explcito das tabelas (conforme script Oracle)
            modelBuilder.Entity<Role>().ToTable("ROLES");
            modelBuilder.Entity<User>().ToTable("USERS_ORBIT");
            modelBuilder.Entity<DestinationType>().ToTable("DESTINATION_TYPES");
            modelBuilder.Entity<Destination>().ToTable("DESTINATIONS");
            modelBuilder.Entity<BookingStatus>().ToTable("BOOKING_STATUSES");
            modelBuilder.Entity<Booking>().ToTable("BOOKINGS");
            modelBuilder.Entity<Passenger>().ToTable("PASSENGERS"); 
            modelBuilder.Entity<Payment>().ToTable("PAYMENTS");
            modelBuilder.Entity<Review>().ToTable("REVIEWS");
            modelBuilder.Entity<AiRecommendation>().ToTable("AI_RECOMMENDATIONS");

            // Ignorando Tabela Ticket que não existe no SQL Novo
            modelBuilder.Ignore<Ticket>();
            
            // Configuração das propriedades decimais devido ao warning do Oracle EF Core
            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Destination>()
                .Property(d => d.BasePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            // Configurações e restrições EXTRAS
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Destination)
                .WithMany(d => d.Bookings)
                .HasForeignKey(b => b.DestinationId);

            modelBuilder.Entity<Passenger>()
                .HasOne(p => p.Booking)
                .WithMany(b => b.Passengers)
                .HasForeignKey(p => p.BookingId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId);
        }
    }
}