using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental_backend.Migrations
{
    public partial class moalcreio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    offer_price = table.Column<double>(type: "float", nullable: false),
                    Users_id = table.Column<int>(type: "int", nullable: false),
                    Cars_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Cars_Cars_id",
                        column: x => x.Cars_id,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Users_Users_id",
                        column: x => x.Users_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    notification_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Offer_id = table.Column<int>(type: "int", nullable: true),
                    Users_id = table.Column<int>(type: "int", nullable: false),
                    Cars_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Cars_Cars_id",
                        column: x => x.Cars_id,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Offers_Offer_id",
                        column: x => x.Offer_id,
                        principalTable: "Offers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notification_Users_Users_id",
                        column: x => x.Users_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Cars_id",
                table: "Notification",
                column: "Cars_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Offer_id",
                table: "Notification",
                column: "Offer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Users_id",
                table: "Notification",
                column: "Users_id");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_Cars_id",
                table: "Offers",
                column: "Cars_id");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_Users_id",
                table: "Offers",
                column: "Users_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Offers");
        }
    }
}
