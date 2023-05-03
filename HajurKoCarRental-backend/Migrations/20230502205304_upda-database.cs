using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental_backend.Migrations
{
    public partial class updadatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamagedCars_Cars_carsId",
                table: "DamagedCars");

            migrationBuilder.DropForeignKey(
                name: "FK_DamagedCars_Users_usersId",
                table: "DamagedCars");

            migrationBuilder.DropColumn(
                name: "discount",
                table: "RentalRequest");

            migrationBuilder.RenameColumn(
                name: "usersId",
                table: "DamagedCars",
                newName: "Users_id");

            migrationBuilder.RenameColumn(
                name: "carsId",
                table: "DamagedCars",
                newName: "Cars_id");

            migrationBuilder.RenameIndex(
                name: "IX_DamagedCars_usersId",
                table: "DamagedCars",
                newName: "IX_DamagedCars_Users_id");

            migrationBuilder.RenameIndex(
                name: "IX_DamagedCars_carsId",
                table: "DamagedCars",
                newName: "IX_DamagedCars_Cars_id");

            migrationBuilder.AddForeignKey(
                name: "FK_DamagedCars_Cars_Cars_id",
                table: "DamagedCars",
                column: "Cars_id",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamagedCars_Users_Users_id",
                table: "DamagedCars",
                column: "Users_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamagedCars_Cars_Cars_id",
                table: "DamagedCars");

            migrationBuilder.DropForeignKey(
                name: "FK_DamagedCars_Users_Users_id",
                table: "DamagedCars");

            migrationBuilder.RenameColumn(
                name: "Users_id",
                table: "DamagedCars",
                newName: "usersId");

            migrationBuilder.RenameColumn(
                name: "Cars_id",
                table: "DamagedCars",
                newName: "carsId");

            migrationBuilder.RenameIndex(
                name: "IX_DamagedCars_Users_id",
                table: "DamagedCars",
                newName: "IX_DamagedCars_usersId");

            migrationBuilder.RenameIndex(
                name: "IX_DamagedCars_Cars_id",
                table: "DamagedCars",
                newName: "IX_DamagedCars_carsId");

            migrationBuilder.AddColumn<double>(
                name: "discount",
                table: "RentalRequest",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_DamagedCars_Cars_carsId",
                table: "DamagedCars",
                column: "carsId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamagedCars_Users_usersId",
                table: "DamagedCars",
                column: "usersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
