using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamunTutorial.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UsersInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpirationDateTime",
                table: "UsersInfo",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpirationDateTime",
                table: "UsersInfo");
        }
    }
}
