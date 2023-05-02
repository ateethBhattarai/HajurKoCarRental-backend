using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental_backend.Migrations
{
    public partial class milagea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "model_name",
                table: "Cars",
                newName: "model");

            migrationBuilder.RenameColumn(
                name: "brand_name",
                table: "Cars",
                newName: "brand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "model",
                table: "Cars",
                newName: "model_name");

            migrationBuilder.RenameColumn(
                name: "brand",
                table: "Cars",
                newName: "brand_name");
        }
    }
}
