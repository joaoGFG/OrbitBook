using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrbitBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataWithAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BOOKING_STATUSES",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Reserva criada aguardando confirmação de pagamento", "PENDENTE" },
                    { 2, "Pagamento aprovado e reserva confirmada", "CONFIRMADO" },
                    { 3, "Missão em andamento — viajante em trânsito", "EM_MISSAO" },
                    { 4, "Missão concluída com sucesso e retorno confirmado", "CONCLUIDO" },
                    { 5, "Reserva cancelada pelo viajante ou pela operadora", "CANCELADO" }
                });

            migrationBuilder.InsertData(
                table: "DESTINATION_TYPES",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Voo acima de 100km com experiência de microgravidade por até 30 minutos", "SUBORBITAL" },
                    { 2, "Órbita baixa terrestre entre 350km e 420km de altitude", "ORBITA_LEO" },
                    { 3, "Missões para flyby ou pouso na superfície lunar", "LUNAR" },
                    { 4, "Missões de longa duração para colonização de Marte", "MARCIANO" }
                });

            migrationBuilder.InsertData(
                table: "ROLES",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Usuário final que busca e reserva experiências espaciais", "VIAJANTE" },
                    { 2, "Administrador com acesso total à plataforma OrbitBook", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "DESTINATIONS",
                columns: new[] { "Id", "AvailableSeats", "BasePrice", "Capacity", "Description", "DistanceKm", "ImageUrl", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, 6, 450000m, 6, "Voo suborbital de 11 minutos.", 107L, "https://orbitbook.com/img/new-shepard.jpg", "Blue Origin New Shepard", 1 },
                    { 2, 4, 350000m, 4, "Voo suborbital com decolagem por aeronave.", 88L, "https://orbitbook.com/img/vss-unity.jpg", "Virgin Galactic VSS Unity", 1 },
                    { 3, 4, 55000000m, 4, "Estadia de 14 dias na primeira estação espacial.", 420L, "https://orbitbook.com/img/axiom-station.jpg", "Axiom Station Ax-4", 2 },
                    { 4, 4, 35000000m, 4, "Missão de 7 dias em órbita baixa.", 380L, "https://orbitbook.com/img/crew-dragon.jpg", "SpaceX Crew Dragon LEO", 2 },
                    { 5, 3, 40000000m, 3, "Parceria internacional para estadia de 10 dias.", 390L, "https://orbitbook.com/img/tiangong.jpg", "Estação Orbital Chinesa Tiangong", 2 },
                    { 6, 2, 150000000m, 2, "Missão de 8 dias ao redor da Lua sem pouso.", 384400L, "https://orbitbook.com/img/artemis-flyby.jpg", "Artemis Lunar Flyby", 3 },
                    { 7, 2, 250000000m, 2, "Pouso na superfície lunar por 5 dias.", 384400L, "https://orbitbook.com/img/lunar-gateway.jpg", "Missão Lunar Gateway Alpha", 3 },
                    { 8, 12, 500000000m, 12, "Missão pioneira de 2 anos para colonização.", 225000000L, "https://orbitbook.com/img/mars-colony.jpg", "SpaceX Mars Colony — Alpha Wave", 4 }
                });

            migrationBuilder.InsertData(
                table: "USERS_ORBIT",
                columns: new[] { "Id", "DocumentNumber", "Email", "Name", "PasswordHash", "RoleId" },
                values: new object[] { 1, "00000000000", "admin@orbitbook.com", "Admin OrbitBook", "$2a$11$/tKJLtMjYT4LSSfGaiFdyOM6X5fnzM1q1vEMEacM0uReqC2Wsg1sG", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BOOKING_STATUSES",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BOOKING_STATUSES",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BOOKING_STATUSES",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BOOKING_STATUSES",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BOOKING_STATUSES",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DESTINATIONS",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DESTINATIONS",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DESTINATIONS",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DESTINATIONS",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DESTINATIONS",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DESTINATIONS",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DESTINATIONS",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DESTINATIONS",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "USERS_ORBIT",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DESTINATION_TYPES",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DESTINATION_TYPES",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DESTINATION_TYPES",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DESTINATION_TYPES",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
