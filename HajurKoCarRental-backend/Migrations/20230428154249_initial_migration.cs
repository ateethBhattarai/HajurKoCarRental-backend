using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental_backend.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    model_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    registration_number = table.Column<int>(type: "int", nullable: false),
                    brand_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rental_cost = table.Column<int>(type: "int", nullable: false),
                    availability_status = table.Column<int>(type: "int", nullable: false),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    profile_picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DamagedCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    damage_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    settlement_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    repair_cost = table.Column<int>(type: "int", nullable: false),
                    usersId = table.Column<int>(type: "int", nullable: false),
                    carsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamagedCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamagedCars_Cars_carsId",
                        column: x => x.carsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DamagedCars_Users_usersId",
                        column: x => x.usersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    carsId = table.Column<int>(type: "int", nullable: false),
                    requested_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    authorized_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rental_duration = table.Column<int>(type: "int", nullable: false),
                    rental_charge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalHistory_Cars_carsId",
                        column: x => x.carsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalHistory_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rental_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    discount = table.Column<bool>(type: "bit", nullable: false),
                    rental_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rental_amount = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    carsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalRequest_Cars_carsId",
                        column: x => x.carsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalRequest_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DamagedCars_carsId",
                table: "DamagedCars",
                column: "carsId");

            migrationBuilder.CreateIndex(
                name: "IX_DamagedCars_usersId",
                table: "DamagedCars",
                column: "usersId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalHistory_carsId",
                table: "RentalHistory",
                column: "carsId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalHistory_userId",
                table: "RentalHistory",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequest_carsId",
                table: "RentalRequest",
                column: "carsId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequest_userId",
                table: "RentalRequest",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamagedCars");

            migrationBuilder.DropTable(
                name: "RentalHistory");

            migrationBuilder.DropTable(
                name: "RentalRequest");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
