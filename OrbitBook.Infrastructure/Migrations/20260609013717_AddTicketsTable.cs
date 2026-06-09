using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TICKETS_BOOKINGS_BookingId",
                table: "TICKETS");

            migrationBuilder.DropForeignKey(
                name: "FK_TICKETS_PASSENGERS_PassengerId",
                table: "TICKETS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TICKETS",
                table: "TICKETS");

            migrationBuilder.RenameTable(
                name: "TICKETS",
                newName: "Tickets");

            migrationBuilder.RenameIndex(
                name: "IX_TICKETS_PassengerId",
                table: "Tickets",
                newName: "IX_Tickets_PassengerId");

            migrationBuilder.RenameIndex(
                name: "IX_TICKETS_BookingId",
                table: "Tickets",
                newName: "IX_Tickets_BookingId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PAYMENTS",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "DESTINATIONS",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "BOOKINGS",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_BOOKINGS_BookingId",
                table: "Tickets",
                column: "BookingId",
                principalTable: "BOOKINGS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_PASSENGERS_PassengerId",
                table: "Tickets",
                column: "PassengerId",
                principalTable: "PASSENGERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_BOOKINGS_BookingId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_PASSENGERS_PassengerId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "TICKETS");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_PassengerId",
                table: "TICKETS",
                newName: "IX_TICKETS_PassengerId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_BookingId",
                table: "TICKETS",
                newName: "IX_TICKETS_BookingId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PAYMENTS",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "DESTINATIONS",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "BOOKINGS",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TICKETS",
                table: "TICKETS",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_BOOKINGS_BookingId",
                table: "TICKETS",
                column: "BookingId",
                principalTable: "BOOKINGS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_PASSENGERS_PassengerId",
                table: "TICKETS",
                column: "PassengerId",
                principalTable: "PASSENGERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
