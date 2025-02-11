using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamunTutorial.Migrations
{
    /// <inheritdoc />
    public partial class logsssddder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UsersInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInfo",
                table: "UsersInfo",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInfo",
                table: "UsersInfo");

            migrationBuilder.RenameTable(
                name: "UsersInfo",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");
        }
    }
}
