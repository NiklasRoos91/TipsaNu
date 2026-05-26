using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipsaNu.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTournamentCompetitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentCompetitors",
                columns: table => new
                {
                    TournamentCompetitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    CompetitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentCompetitors", x => x.TournamentCompetitorId);
                    table.ForeignKey(
                        name: "FK_TournamentCompetitors_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalSchema: "dbo",
                        principalTable: "Competitors",
                        principalColumn: "CompetitorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentCompetitors_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalSchema: "dbo",
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentCompetitors_CompetitorId",
                table: "TournamentCompetitors",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentCompetitors_TournamentId",
                table: "TournamentCompetitors",
                column: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentCompetitors");
        }
    }
}
