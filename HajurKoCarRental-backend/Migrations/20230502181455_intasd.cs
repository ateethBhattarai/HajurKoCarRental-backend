using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental_backend.Migrations
{
    public partial class intasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "available_discount",
                table: "RentalRequest");

            migrationBuilder.AddColumn<double>(
                name: "discount",
                table: "RentalRequest",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount",
                table: "RentalRequest");

            migrationBuilder.AddColumn<bool>(
                name: "available_discount",
                table: "RentalRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
