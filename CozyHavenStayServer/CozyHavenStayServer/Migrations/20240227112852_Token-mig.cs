using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyHavenStayServer.Migrations
{
    public partial class Tokenmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordExpires",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordExpires",
                table: "HotelOwners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "HotelOwners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordExpires",
                table: "Admin",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordExpires",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetPasswordExpires",
                table: "HotelOwners");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "HotelOwners");

            migrationBuilder.DropColumn(
                name: "ResetPasswordExpires",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Admin");
        }
    }
}
