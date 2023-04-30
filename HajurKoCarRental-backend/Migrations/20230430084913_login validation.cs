using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental_backend.Migrations
{
    public partial class loginvalidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequest_Cars_carsId",
                table: "RentalRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequest_Users_userId",
                table: "RentalRequest");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "discount_percentage",
                table: "RentalRequest");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "RentalRequest",
                newName: "Users_id");

            migrationBuilder.RenameColumn(
                name: "carsId",
                table: "RentalRequest",
                newName: "Cars_id");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequest_userId",
                table: "RentalRequest",
                newName: "IX_RentalRequest_Users_id");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequest_carsId",
                table: "RentalRequest",
                newName: "IX_RentalRequest_Cars_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<double>(
                name: "rental_amount",
                table: "RentalRequest",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "rental_cost",
                table: "Cars",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequest_Cars_Cars_id",
                table: "RentalRequest",
                column: "Cars_id",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequest_Users_Users_id",
                table: "RentalRequest",
                column: "Users_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequest_Cars_Cars_id",
                table: "RentalRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequest_Users_Users_id",
                table: "RentalRequest");

            migrationBuilder.DropColumn(
                name: "last_login",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Users_id",
                table: "RentalRequest",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Cars_id",
                table: "RentalRequest",
                newName: "carsId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequest_Users_id",
                table: "RentalRequest",
                newName: "IX_RentalRequest_userId");

            migrationBuilder.RenameIndex(
                name: "IX_RentalRequest_Cars_id",
                table: "RentalRequest",
                newName: "IX_RentalRequest_carsId");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "rental_amount",
                table: "RentalRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "discount_percentage",
                table: "RentalRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "rental_cost",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequest_Cars_carsId",
                table: "RentalRequest",
                column: "carsId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequest_Users_userId",
                table: "RentalRequest",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
