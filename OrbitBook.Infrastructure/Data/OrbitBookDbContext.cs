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
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ─── MAPEAMENTO EXPLÍCITO DAS TABELAS ──────────────────────────
            modelBuilder.Entity<Role>().ToTable("ROLES");
            modelBuilder.Entity<User>().ToTable("USERS_ORBIT");
            modelBuilder.Entity<DestinationType>().ToTable("DESTINATION_TYPES");
            modelBuilder.Entity<Destination>().ToTable("DESTINATIONS");
            modelBuilder.Entity<BookingStatus>().ToTable("BOOKING_STATUSES");
            modelBuilder.Entity<Booking>().ToTable("BOOKINGS");
            modelBuilder.Entity<Passenger>().ToTable("PASSENGERS");
            modelBuilder.Entity<Ticket>().ToTable("TICKETS");
            modelBuilder.Entity<Review>().ToTable("REVIEWS");

            // ─── CONFIGURAÇÃO DE PROPRIEDADES DECIMAIS ─────────────────────
            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Destination>()
                .Property(d => d.BasePrice)
                .HasColumnType("decimal(18,2)");

            // ─── CONFIGURAÇÕES E RESTRIÇÕES EXTRAS (CHAVES ESTRANGEIRAS) ───
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

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Status)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.StatusId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Booking)
                .WithOne(b => b.Review)
                .HasForeignKey<Review>(r => r.BookingId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Booking)
                .WithMany()
                .HasForeignKey(t => t.BookingId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Passenger)
                .WithMany()
                .HasForeignKey(t => t.PassengerId);

            // ─── SEED DATA (DADOS INICIAIS) ────────────────────────────────
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "VIAJANTE", Description = "Usuário final que busca e reserva experiências espaciais" },
                new Role { Id = 2, Name = "ADMIN", Description = "Administrador com acesso total à plataforma OrbitBook" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    RoleId = 2,
                    Name = "Admin OrbitBook",
                    Email = "admin@orbitbook.com",
                    PasswordHash = "$2a$11$/tKJLtMjYT4LSSfGaiFdyOM6X5fnzM1q1vEMEacM0uReqC2Wsg1sG",
                    DocumentNumber = "00000000000"
                }
            );

            modelBuilder.Entity<BookingStatus>().HasData(
                new BookingStatus { Id = 1, Name = "PENDENTE", Description = "Reserva criada aguardando confirmação de pagamento" },
                new BookingStatus { Id = 2, Name = "CONFIRMADO", Description = "Pagamento aprovado e reserva confirmada" },
                new BookingStatus { Id = 3, Name = "EM_MISSAO", Description = "Missão em andamento — viajante em trânsito" },
                new BookingStatus { Id = 4, Name = "CONCLUIDO", Description = "Missão concluída com sucesso e retorno confirmado" },
                new BookingStatus { Id = 5, Name = "CANCELADO", Description = "Reserva cancelada pelo viajante ou pela operadora" }
            );

            modelBuilder.Entity<DestinationType>().HasData(
                new DestinationType { Id = 1, Name = "SUBORBITAL", Description = "Voo acima de 100km com experiência de microgravidade por até 30 minutos" },
                new DestinationType { Id = 2, Name = "ORBITA_LEO", Description = "Órbita baixa terrestre entre 350km e 420km de altitude" },
                new DestinationType { Id = 3, Name = "LUNAR", Description = "Missões para flyby ou pouso na superfície lunar" },
                new DestinationType { Id = 4, Name = "MARCIANO", Description = "Missões de longa duração para colonização de Marte" }
            );

            modelBuilder.Entity<Destination>().HasData(
                new Destination { Id = 1, TypeId = 1, Name = "Blue Origin New Shepard", Description = "Voo suborbital de 11 minutos.", DistanceKm = 107L, BasePrice = 450000m, Capacity = 6, AvailableSeats = 6, ImageUrl = "https://orbitbook.com/img/new-shepard.jpg" },
                new Destination { Id = 2, TypeId = 1, Name = "Virgin Galactic VSS Unity", Description = "Voo suborbital com decolagem por aeronave.", DistanceKm = 88L, BasePrice = 350000m, Capacity = 4, AvailableSeats = 4, ImageUrl = "https://orbitbook.com/img/vss-unity.jpg" },
                new Destination { Id = 3, TypeId = 2, Name = "Axiom Station Ax-4", Description = "Estadia de 14 dias na primeira estação espacial.", DistanceKm = 420L, BasePrice = 55000000m, Capacity = 4, AvailableSeats = 4, ImageUrl = "https://orbitbook.com/img/axiom-station.jpg" },
                new Destination { Id = 4, TypeId = 2, Name = "SpaceX Crew Dragon LEO", Description = "Missão de 7 dias em órbita baixa.", DistanceKm = 380L, BasePrice = 35000000m, Capacity = 4, AvailableSeats = 4, ImageUrl = "https://orbitbook.com/img/crew-dragon.jpg" },
                new Destination { Id = 5, TypeId = 2, Name = "Estação Orbital Chinesa Tiangong", Description = "Parceria internacional para estadia de 10 dias.", DistanceKm = 390L, BasePrice = 40000000m, Capacity = 3, AvailableSeats = 3, ImageUrl = "https://orbitbook.com/img/tiangong.jpg" },
                new Destination { Id = 6, TypeId = 3, Name = "Artemis Lunar Flyby", Description = "Missão de 8 dias ao redor da Lua sem pouso.", DistanceKm = 384400L, BasePrice = 150000000m, Capacity = 2, AvailableSeats = 2, ImageUrl = "https://orbitbook.com/img/artemis-flyby.jpg" },
                new Destination { Id = 7, TypeId = 3, Name = "Missão Lunar Gateway Alpha", Description = "Pouso na superfície lunar por 5 dias.", DistanceKm = 384400L, BasePrice = 250000000m, Capacity = 2, AvailableSeats = 2, ImageUrl = "https://orbitbook.com/img/lunar-gateway.jpg" },
                new Destination { Id = 8, TypeId = 4, Name = "SpaceX Mars Colony — Alpha Wave", Description = "Missão pioneira de 2 anos para colonização.", DistanceKm = 225000000L, BasePrice = 500000000m, Capacity = 12, AvailableSeats = 12, ImageUrl = "https://orbitbook.com/img/mars-colony.jpg" }
            );
        }
    }
}