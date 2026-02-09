using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipsaNu.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAllowCustomChoiceToExtraBetOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowCustomChoice",
                schema: "dbo",
                table: "ExtraBetOptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowCustomChoice",
                schema: "dbo",
                table: "ExtraBetOptions");
        }
    }
}
