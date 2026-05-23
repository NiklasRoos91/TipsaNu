using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipsaNu.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToDotNet10AndCleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "dbo",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Wins",
                schema: "dbo",
                table: "GroupStanding",
                newName: "Won");

            migrationBuilder.RenameColumn(
                name: "Losses",
                schema: "dbo",
                table: "GroupStanding",
                newName: "Lost");

            migrationBuilder.RenameColumn(
                name: "Draws",
                schema: "dbo",
                table: "GroupStanding",
                newName: "Draw");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "dbo",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Won",
                schema: "dbo",
                table: "GroupStanding",
                newName: "Wins");

            migrationBuilder.RenameColumn(
                name: "Lost",
                schema: "dbo",
                table: "GroupStanding",
                newName: "Losses");

            migrationBuilder.RenameColumn(
                name: "Draw",
                schema: "dbo",
                table: "GroupStanding",
                newName: "Draws");
        }
    }
}
