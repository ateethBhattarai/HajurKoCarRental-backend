using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental_backend.Migrations
{
    public partial class all : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalHistory_Cars_carsId",
                table: "RentalHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalHistory_Users_userId",
                table: "RentalHistory");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "rental_date",
                table: "RentalRequest",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "RentalHistory",
                newName: "Users_id");

            migrationBuilder.RenameColumn(
                name: "carsId",
                table: "RentalHistory",
                newName: "Cars_id");

            migrationBuilder.RenameIndex(
                name: "IX_RentalHistory_userId",
                table: "RentalHistory",
                newName: "IX_RentalHistory_Users_id");

            migrationBuilder.RenameIndex(
                name: "IX_RentalHistory_carsId",
                table: "RentalHistory",
                newName: "IX_RentalHistory_Cars_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "end_date",
                table: "RentalRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<double>(
                name: "rental_charge",
                table: "RentalHistory",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHistory_Cars_Cars_id",
                table: "RentalHistory",
                column: "Cars_id",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHistory_Users_Users_id",
                table: "RentalHistory",
                column: "Users_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalHistory_Cars_Cars_id",
                table: "RentalHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalHistory_Users_Users_id",
                table: "RentalHistory");

            migrationBuilder.DropColumn(
                name: "end_date",
                table: "RentalRequest");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "RentalRequest",
                newName: "rental_date");

            migrationBuilder.RenameColumn(
                name: "Users_id",
                table: "RentalHistory",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Cars_id",
                table: "RentalHistory",
                newName: "carsId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalHistory_Users_id",
                table: "RentalHistory",
                newName: "IX_RentalHistory_userId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalHistory_Cars_id",
                table: "RentalHistory",
                newName: "IX_RentalHistory_carsId");

            migrationBuilder.AlterColumn<int>(
                name: "rental_charge",
                table: "RentalHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHistory_Cars_carsId",
                table: "RentalHistory",
                column: "carsId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalHistory_Users_userId",
                table: "RentalHistory",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
