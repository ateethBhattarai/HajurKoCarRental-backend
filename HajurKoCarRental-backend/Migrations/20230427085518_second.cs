using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental_backend.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "full_name");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_birth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    model_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    model_number = table.Column<int>(type: "int", nullable: false),
                    rental_amount = table.Column<int>(type: "int", nullable: false),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DamagedCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    damaged_parts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    settlement_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamagedCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rental_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    discount = table.Column<bool>(type: "bit", nullable: false),
                    rental_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rental_amount = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    carsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Cars_carsId",
                        column: x => x.carsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_carsId",
                table: "Rentals",
                column: "carsId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_userid",
                table: "Rentals",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamagedCars");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "Users",
                newName: "name");
        }
    }
}
