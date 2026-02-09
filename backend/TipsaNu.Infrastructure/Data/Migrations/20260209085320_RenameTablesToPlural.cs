using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipsaNu.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesToPlural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBetOption_Match_MatchId",
                schema: "dbo",
                table: "ExtraBetOption");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBetOption_Tournaments_TournamentId",
                schema: "dbo",
                table: "ExtraBetOption");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBetOptionChoice_ExtraBetOption_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionChoice");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBetOptionCorrectValue_ExtraBetOption_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBets_ExtraBetOption_OptionId",
                schema: "dbo",
                table: "ExtraBets");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Competitors_AwayCompetitorId",
                schema: "dbo",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Competitors_HomeCompetitorId",
                schema: "dbo",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Competitors_WinnerCompetitorId",
                schema: "dbo",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Groups_GroupId",
                schema: "dbo",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Match_DependsOnMatch1Id",
                schema: "dbo",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Match_DependsOnMatch2Id",
                schema: "dbo",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Tournaments_TournamentId",
                schema: "dbo",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Match_MatchId",
                schema: "dbo",
                table: "Predictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                schema: "dbo",
                table: "Match");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraBetOptionCorrectValue",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraBetOptionChoice",
                schema: "dbo",
                table: "ExtraBetOptionChoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraBetOption",
                schema: "dbo",
                table: "ExtraBetOption");

            migrationBuilder.RenameTable(
                name: "Match",
                schema: "dbo",
                newName: "Matches",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ExtraBetOptionCorrectValue",
                schema: "dbo",
                newName: "ExtraBetOptionCorrectValues",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ExtraBetOptionChoice",
                schema: "dbo",
                newName: "ExtraBetOptionChoices",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ExtraBetOption",
                schema: "dbo",
                newName: "ExtraBetOptions",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Match_WinnerCompetitorId",
                schema: "dbo",
                table: "Matches",
                newName: "IX_Matches_WinnerCompetitorId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_TournamentId",
                schema: "dbo",
                table: "Matches",
                newName: "IX_Matches_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_HomeCompetitorId",
                schema: "dbo",
                table: "Matches",
                newName: "IX_Matches_HomeCompetitorId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_GroupId",
                schema: "dbo",
                table: "Matches",
                newName: "IX_Matches_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_DependsOnMatch2Id",
                schema: "dbo",
                table: "Matches",
                newName: "IX_Matches_DependsOnMatch2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Match_DependsOnMatch1Id",
                schema: "dbo",
                table: "Matches",
                newName: "IX_Matches_DependsOnMatch1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Match_AwayCompetitorId",
                schema: "dbo",
                table: "Matches",
                newName: "IX_Matches_AwayCompetitorId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraBetOptionCorrectValue_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValues",
                newName: "IX_ExtraBetOptionCorrectValues_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraBetOptionChoice_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionChoices",
                newName: "IX_ExtraBetOptionChoices_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraBetOption_TournamentId",
                schema: "dbo",
                table: "ExtraBetOptions",
                newName: "IX_ExtraBetOptions_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraBetOption_MatchId",
                schema: "dbo",
                table: "ExtraBetOptions",
                newName: "IX_ExtraBetOptions_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                schema: "dbo",
                table: "Matches",
                column: "MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraBetOptionCorrectValues",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValues",
                column: "CorrectValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraBetOptionChoices",
                schema: "dbo",
                table: "ExtraBetOptionChoices",
                column: "ChoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraBetOptions",
                schema: "dbo",
                table: "ExtraBetOptions",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBetOptionChoices_ExtraBetOptions_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionChoices",
                column: "OptionId",
                principalSchema: "dbo",
                principalTable: "ExtraBetOptions",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBetOptionCorrectValues_ExtraBetOptions_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValues",
                column: "OptionId",
                principalSchema: "dbo",
                principalTable: "ExtraBetOptions",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBetOptions_Matches_MatchId",
                schema: "dbo",
                table: "ExtraBetOptions",
                column: "MatchId",
                principalSchema: "dbo",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBetOptions_Tournaments_TournamentId",
                schema: "dbo",
                table: "ExtraBetOptions",
                column: "TournamentId",
                principalSchema: "dbo",
                principalTable: "Tournaments",
                principalColumn: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBets_ExtraBetOptions_OptionId",
                schema: "dbo",
                table: "ExtraBets",
                column: "OptionId",
                principalSchema: "dbo",
                principalTable: "ExtraBetOptions",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Competitors_AwayCompetitorId",
                schema: "dbo",
                table: "Matches",
                column: "AwayCompetitorId",
                principalSchema: "dbo",
                principalTable: "Competitors",
                principalColumn: "CompetitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Competitors_HomeCompetitorId",
                schema: "dbo",
                table: "Matches",
                column: "HomeCompetitorId",
                principalSchema: "dbo",
                principalTable: "Competitors",
                principalColumn: "CompetitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Competitors_WinnerCompetitorId",
                schema: "dbo",
                table: "Matches",
                column: "WinnerCompetitorId",
                principalSchema: "dbo",
                principalTable: "Competitors",
                principalColumn: "CompetitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Groups_GroupId",
                schema: "dbo",
                table: "Matches",
                column: "GroupId",
                principalSchema: "dbo",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_DependsOnMatch1Id",
                schema: "dbo",
                table: "Matches",
                column: "DependsOnMatch1Id",
                principalSchema: "dbo",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_DependsOnMatch2Id",
                schema: "dbo",
                table: "Matches",
                column: "DependsOnMatch2Id",
                principalSchema: "dbo",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                schema: "dbo",
                table: "Matches",
                column: "TournamentId",
                principalSchema: "dbo",
                principalTable: "Tournaments",
                principalColumn: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Matches_MatchId",
                schema: "dbo",
                table: "Predictions",
                column: "MatchId",
                principalSchema: "dbo",
                principalTable: "Matches",
                principalColumn: "MatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBetOptionChoices_ExtraBetOptions_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionChoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBetOptionCorrectValues_ExtraBetOptions_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBetOptions_Matches_MatchId",
                schema: "dbo",
                table: "ExtraBetOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBetOptions_Tournaments_TournamentId",
                schema: "dbo",
                table: "ExtraBetOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraBets_ExtraBetOptions_OptionId",
                schema: "dbo",
                table: "ExtraBets");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Competitors_AwayCompetitorId",
                schema: "dbo",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Competitors_HomeCompetitorId",
                schema: "dbo",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Competitors_WinnerCompetitorId",
                schema: "dbo",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Groups_GroupId",
                schema: "dbo",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_DependsOnMatch1Id",
                schema: "dbo",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_DependsOnMatch2Id",
                schema: "dbo",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                schema: "dbo",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Matches_MatchId",
                schema: "dbo",
                table: "Predictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                schema: "dbo",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraBetOptions",
                schema: "dbo",
                table: "ExtraBetOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraBetOptionCorrectValues",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraBetOptionChoices",
                schema: "dbo",
                table: "ExtraBetOptionChoices");

            migrationBuilder.RenameTable(
                name: "Matches",
                schema: "dbo",
                newName: "Match",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ExtraBetOptions",
                schema: "dbo",
                newName: "ExtraBetOption",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ExtraBetOptionCorrectValues",
                schema: "dbo",
                newName: "ExtraBetOptionCorrectValue",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ExtraBetOptionChoices",
                schema: "dbo",
                newName: "ExtraBetOptionChoice",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_WinnerCompetitorId",
                schema: "dbo",
                table: "Match",
                newName: "IX_Match_WinnerCompetitorId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_TournamentId",
                schema: "dbo",
                table: "Match",
                newName: "IX_Match_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_HomeCompetitorId",
                schema: "dbo",
                table: "Match",
                newName: "IX_Match_HomeCompetitorId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_GroupId",
                schema: "dbo",
                table: "Match",
                newName: "IX_Match_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_DependsOnMatch2Id",
                schema: "dbo",
                table: "Match",
                newName: "IX_Match_DependsOnMatch2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_DependsOnMatch1Id",
                schema: "dbo",
                table: "Match",
                newName: "IX_Match_DependsOnMatch1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_AwayCompetitorId",
                schema: "dbo",
                table: "Match",
                newName: "IX_Match_AwayCompetitorId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraBetOptions_TournamentId",
                schema: "dbo",
                table: "ExtraBetOption",
                newName: "IX_ExtraBetOption_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraBetOptions_MatchId",
                schema: "dbo",
                table: "ExtraBetOption",
                newName: "IX_ExtraBetOption_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraBetOptionCorrectValues_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValue",
                newName: "IX_ExtraBetOptionCorrectValue_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraBetOptionChoices_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionChoice",
                newName: "IX_ExtraBetOptionChoice_OptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                schema: "dbo",
                table: "Match",
                column: "MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraBetOption",
                schema: "dbo",
                table: "ExtraBetOption",
                column: "OptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraBetOptionCorrectValue",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValue",
                column: "CorrectValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraBetOptionChoice",
                schema: "dbo",
                table: "ExtraBetOptionChoice",
                column: "ChoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBetOption_Match_MatchId",
                schema: "dbo",
                table: "ExtraBetOption",
                column: "MatchId",
                principalSchema: "dbo",
                principalTable: "Match",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBetOption_Tournaments_TournamentId",
                schema: "dbo",
                table: "ExtraBetOption",
                column: "TournamentId",
                principalSchema: "dbo",
                principalTable: "Tournaments",
                principalColumn: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBetOptionChoice_ExtraBetOption_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionChoice",
                column: "OptionId",
                principalSchema: "dbo",
                principalTable: "ExtraBetOption",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBetOptionCorrectValue_ExtraBetOption_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValue",
                column: "OptionId",
                principalSchema: "dbo",
                principalTable: "ExtraBetOption",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraBets_ExtraBetOption_OptionId",
                schema: "dbo",
                table: "ExtraBets",
                column: "OptionId",
                principalSchema: "dbo",
                principalTable: "ExtraBetOption",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Competitors_AwayCompetitorId",
                schema: "dbo",
                table: "Match",
                column: "AwayCompetitorId",
                principalSchema: "dbo",
                principalTable: "Competitors",
                principalColumn: "CompetitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Competitors_HomeCompetitorId",
                schema: "dbo",
                table: "Match",
                column: "HomeCompetitorId",
                principalSchema: "dbo",
                principalTable: "Competitors",
                principalColumn: "CompetitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Competitors_WinnerCompetitorId",
                schema: "dbo",
                table: "Match",
                column: "WinnerCompetitorId",
                principalSchema: "dbo",
                principalTable: "Competitors",
                principalColumn: "CompetitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Groups_GroupId",
                schema: "dbo",
                table: "Match",
                column: "GroupId",
                principalSchema: "dbo",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Match_DependsOnMatch1Id",
                schema: "dbo",
                table: "Match",
                column: "DependsOnMatch1Id",
                principalSchema: "dbo",
                principalTable: "Match",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Match_DependsOnMatch2Id",
                schema: "dbo",
                table: "Match",
                column: "DependsOnMatch2Id",
                principalSchema: "dbo",
                principalTable: "Match",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Tournaments_TournamentId",
                schema: "dbo",
                table: "Match",
                column: "TournamentId",
                principalSchema: "dbo",
                principalTable: "Tournaments",
                principalColumn: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Match_MatchId",
                schema: "dbo",
                table: "Predictions",
                column: "MatchId",
                principalSchema: "dbo",
                principalTable: "Match",
                principalColumn: "MatchId");
        }
    }
}
