using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyHavenStayServer.Migrations
{
    public partial class CozyHeavenStayMigV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Hotels_HotelId",
                table: "Booking");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Hotel",
                table: "Booking",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Hotel",
                table: "Booking");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Hotels_HotelId",
                table: "Booking",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId");
        }
    }
}
