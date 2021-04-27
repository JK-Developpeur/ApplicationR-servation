using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationApp.Migrations
{
    public partial class filt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_FiltredId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_FiltredId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "FiltredId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "filtering",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FiltredId",
                table: "Reservation",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "filtering",
                table: "AspNetUsers",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_FiltredId",
                table: "Reservation",
                column: "FiltredId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_FiltredId",
                table: "Reservation",
                column: "FiltredId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
