using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BOOKING_STATUSES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOKING_STATUSES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DESTINATION_TYPES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DESTINATION_TYPES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DESTINATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TypeId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DistanceKm = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    BasePrice = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    Capacity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AvailableSeats = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ImageUrl = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DESTINATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DESTINATIONS_DESTINATION_TYPES_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DESTINATION_TYPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERS_ORBIT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RoleId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS_ORBIT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USERS_ORBIT_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AI_RECOMMENDATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DestinationId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    PromptUsed = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ResponseText = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ModelUsed = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AI_RECOMMENDATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AI_RECOMMENDATIONS_DESTINATIONS_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "DESTINATIONS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AI_RECOMMENDATIONS_USERS_ORBIT_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS_ORBIT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BOOKINGS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DestinationId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StatusId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    NumPassengers = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOKINGS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BOOKINGS_BOOKING_STATUSES_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BOOKING_STATUSES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BOOKINGS_DESTINATIONS_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "DESTINATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BOOKINGS_USERS_ORBIT_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS_ORBIT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PASSENGERS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    BookingId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FullName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    MedicalStatus = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASSENGERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PASSENGERS_BOOKINGS_BookingId",
                        column: x => x.BookingId,
                        principalTable: "BOOKINGS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENTS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    BookingId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Method = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PAYMENTS_BOOKINGS_BookingId",
                        column: x => x.BookingId,
                        principalTable: "BOOKINGS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REVIEWS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    BookingId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Rating = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Comment = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVIEWS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REVIEWS_BOOKINGS_BookingId",
                        column: x => x.BookingId,
                        principalTable: "BOOKINGS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REVIEWS_USERS_ORBIT_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS_ORBIT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TICKETS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    BookingId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PassengerId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SeatNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TicketClass = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    QrCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TICKETS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TICKETS_BOOKINGS_BookingId",
                        column: x => x.BookingId,
                        principalTable: "BOOKINGS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TICKETS_PASSENGERS_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "PASSENGERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AI_RECOMMENDATIONS_DestinationId",
                table: "AI_RECOMMENDATIONS",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_AI_RECOMMENDATIONS_UserId",
                table: "AI_RECOMMENDATIONS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKINGS_DestinationId",
                table: "BOOKINGS",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKINGS_StatusId",
                table: "BOOKINGS",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKINGS_UserId",
                table: "BOOKINGS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DESTINATIONS_TypeId",
                table: "DESTINATIONS",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PASSENGERS_BookingId",
                table: "PASSENGERS",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENTS_BookingId",
                table: "PAYMENTS",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REVIEWS_BookingId",
                table: "REVIEWS",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REVIEWS_UserId",
                table: "REVIEWS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_BookingId",
                table: "TICKETS",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_PassengerId",
                table: "TICKETS",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_ORBIT_RoleId",
                table: "USERS_ORBIT",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AI_RECOMMENDATIONS");

            migrationBuilder.DropTable(
                name: "PAYMENTS");

            migrationBuilder.DropTable(
                name: "REVIEWS");

            migrationBuilder.DropTable(
                name: "TICKETS");

            migrationBuilder.DropTable(
                name: "PASSENGERS");

            migrationBuilder.DropTable(
                name: "BOOKINGS");

            migrationBuilder.DropTable(
                name: "BOOKING_STATUSES");

            migrationBuilder.DropTable(
                name: "DESTINATIONS");

            migrationBuilder.DropTable(
                name: "USERS_ORBIT");

            migrationBuilder.DropTable(
                name: "DESTINATION_TYPES");

            migrationBuilder.DropTable(
                name: "ROLES");
        }
    }
}
