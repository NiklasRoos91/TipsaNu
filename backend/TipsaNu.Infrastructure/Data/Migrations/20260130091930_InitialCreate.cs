using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TipsaNu.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Competitors",
                schema: "dbo",
                columns: table => new
                {
                    CompetitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsIndividual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitors", x => x.CompetitorId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "User")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentTemplates",
                schema: "dbo",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TotalGroups = table.Column<int>(type: "int", nullable: false),
                    AdvancingPerGroup = table.Column<int>(type: "int", nullable: false),
                    AllowsBestThird = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTemplates", x => x.TemplateId);
                    table.ForeignKey(
                        name: "FK_TournamentTemplates_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PointRules",
                schema: "dbo",
                columns: table => new
                {
                    PointRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    MatchType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Criterion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointRules", x => x.PointRuleId);
                    table.ForeignKey(
                        name: "FK_PointRules_TournamentTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "dbo",
                        principalTable: "TournamentTemplates",
                        principalColumn: "TemplateId");
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                schema: "dbo",
                columns: table => new
                {
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.TournamentId);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "dbo",
                        principalTable: "TournamentTemplates",
                        principalColumn: "TemplateId");
                    table.ForeignKey(
                        name: "FK_Tournaments_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "dbo",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaxTeams = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalSchema: "dbo",
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                schema: "dbo",
                columns: table => new
                {
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AdminUserId = table.Column<int>(type: "int", nullable: false),
                    InvitationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxMembers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueId);
                    table.ForeignKey(
                        name: "FK_Leagues_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalSchema: "dbo",
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                    table.ForeignKey(
                        name: "FK_Leagues_Users_AdminUserId",
                        column: x => x.AdminUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentTiebreakers",
                schema: "dbo",
                columns: table => new
                {
                    TiebreakerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Criterion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTiebreakers", x => x.TiebreakerId);
                    table.ForeignKey(
                        name: "FK_TournamentTiebreakers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalSchema: "dbo",
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                });

            migrationBuilder.CreateTable(
                name: "GroupCompetitors",
                schema: "dbo",
                columns: table => new
                {
                    GroupCompetitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    CompetitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCompetitors", x => x.GroupCompetitorId);
                    table.ForeignKey(
                        name: "FK_GroupCompetitors_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalSchema: "dbo",
                        principalTable: "Competitors",
                        principalColumn: "CompetitorId");
                    table.ForeignKey(
                        name: "FK_GroupCompetitors_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "dbo",
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateTable(
                name: "GroupStanding",
                schema: "dbo",
                columns: table => new
                {
                    StandingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Played = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    GoalDifference = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStanding", x => x.StandingId);
                    table.ForeignKey(
                        name: "FK_GroupStanding_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalSchema: "dbo",
                        principalTable: "Competitors",
                        principalColumn: "CompetitorId");
                    table.ForeignKey(
                        name: "FK_GroupStanding_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "dbo",
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateTable(
                name: "Match",
                schema: "dbo",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    HomeCompetitorId = table.Column<int>(type: "int", nullable: false),
                    AwayCompetitorId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    MatchType = table.Column<int>(type: "int", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PredictionDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScoreHome = table.Column<int>(type: "int", nullable: true),
                    ScoreAway = table.Column<int>(type: "int", nullable: true),
                    WinnerCompetitorId = table.Column<int>(type: "int", nullable: true),
                    DependsOnMatch1Id = table.Column<int>(type: "int", nullable: true),
                    DependsOnMatch2Id = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Match_Competitors_AwayCompetitorId",
                        column: x => x.AwayCompetitorId,
                        principalSchema: "dbo",
                        principalTable: "Competitors",
                        principalColumn: "CompetitorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Competitors_HomeCompetitorId",
                        column: x => x.HomeCompetitorId,
                        principalSchema: "dbo",
                        principalTable: "Competitors",
                        principalColumn: "CompetitorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Competitors_WinnerCompetitorId",
                        column: x => x.WinnerCompetitorId,
                        principalSchema: "dbo",
                        principalTable: "Competitors",
                        principalColumn: "CompetitorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "dbo",
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Match_Match_DependsOnMatch1Id",
                        column: x => x.DependsOnMatch1Id,
                        principalSchema: "dbo",
                        principalTable: "Match",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Match_DependsOnMatch2Id",
                        column: x => x.DependsOnMatch2Id,
                        principalSchema: "dbo",
                        principalTable: "Match",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalSchema: "dbo",
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                });

            migrationBuilder.CreateTable(
                name: "LeagueMembers",
                schema: "dbo",
                columns: table => new
                {
                    LeagueMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueMembers", x => x.LeagueMemberId);
                    table.ForeignKey(
                        name: "FK_LeagueMembers_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalSchema: "dbo",
                        principalTable: "Leagues",
                        principalColumn: "LeagueId");
                    table.ForeignKey(
                        name: "FK_LeagueMembers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ExtraBetOption",
                schema: "dbo",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraBetOption", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_ExtraBetOption_Match_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "dbo",
                        principalTable: "Match",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ExtraBetOption_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalSchema: "dbo",
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                });

            migrationBuilder.CreateTable(
                name: "Predictions",
                schema: "dbo",
                columns: table => new
                {
                    PredictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    PredictedHomeScore = table.Column<int>(type: "int", nullable: false),
                    PredictedAwayScore = table.Column<int>(type: "int", nullable: false),
                    PredictedWinnerId = table.Column<int>(type: "int", nullable: true),
                    PointsAwarded = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.PredictionId);
                    table.ForeignKey(
                        name: "FK_Predictions_Competitors_PredictedWinnerId",
                        column: x => x.PredictedWinnerId,
                        principalSchema: "dbo",
                        principalTable: "Competitors",
                        principalColumn: "CompetitorId");
                    table.ForeignKey(
                        name: "FK_Predictions_Match_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "dbo",
                        principalTable: "Match",
                        principalColumn: "MatchId");
                    table.ForeignKey(
                        name: "FK_Predictions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "LeaderboardEntries",
                schema: "dbo",
                columns: table => new
                {
                    LeagueMemberId = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardEntries", x => x.LeagueMemberId);
                    table.ForeignKey(
                        name: "FK_LeaderboardEntries_LeagueMembers_LeagueMemberId",
                        column: x => x.LeagueMemberId,
                        principalSchema: "dbo",
                        principalTable: "LeagueMembers",
                        principalColumn: "LeagueMemberId");
                });

            migrationBuilder.CreateTable(
                name: "ExtraBetOptionChoice",
                schema: "dbo",
                columns: table => new
                {
                    ChoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraBetOptionChoice", x => x.ChoiceId);
                    table.ForeignKey(
                        name: "FK_ExtraBetOptionChoice_ExtraBetOption_OptionId",
                        column: x => x.OptionId,
                        principalSchema: "dbo",
                        principalTable: "ExtraBetOption",
                        principalColumn: "OptionId");
                });

            migrationBuilder.CreateTable(
                name: "ExtraBetOptionCorrectValue",
                schema: "dbo",
                columns: table => new
                {
                    CorrectValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraBetOptionCorrectValue", x => x.CorrectValueId);
                    table.ForeignKey(
                        name: "FK_ExtraBetOptionCorrectValue_ExtraBetOption_OptionId",
                        column: x => x.OptionId,
                        principalSchema: "dbo",
                        principalTable: "ExtraBetOption",
                        principalColumn: "OptionId");
                });

            migrationBuilder.CreateTable(
                name: "ExtraBets",
                schema: "dbo",
                columns: table => new
                {
                    ExtraBetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PointsAwarded = table.Column<int>(type: "int", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraBets", x => x.ExtraBetId);
                    table.ForeignKey(
                        name: "FK_ExtraBets_ExtraBetOption_OptionId",
                        column: x => x.OptionId,
                        principalSchema: "dbo",
                        principalTable: "ExtraBetOption",
                        principalColumn: "OptionId");
                    table.ForeignKey(
                        name: "FK_ExtraBets_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraBetOption_MatchId",
                schema: "dbo",
                table: "ExtraBetOption",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraBetOption_TournamentId",
                schema: "dbo",
                table: "ExtraBetOption",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraBetOptionChoice_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionChoice",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraBetOptionCorrectValue_OptionId",
                schema: "dbo",
                table: "ExtraBetOptionCorrectValue",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraBets_OptionId",
                schema: "dbo",
                table: "ExtraBets",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraBets_UserId",
                schema: "dbo",
                table: "ExtraBets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCompetitors_CompetitorId",
                schema: "dbo",
                table: "GroupCompetitors",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCompetitors_GroupId_CompetitorId",
                schema: "dbo",
                table: "GroupCompetitors",
                columns: new[] { "GroupId", "CompetitorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TournamentId",
                schema: "dbo",
                table: "Groups",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStanding_CompetitorId",
                schema: "dbo",
                table: "GroupStanding",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStanding_GroupId",
                schema: "dbo",
                table: "GroupStanding",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMembers_LeagueId_UserId",
                schema: "dbo",
                table: "LeagueMembers",
                columns: new[] { "LeagueId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMembers_UserId",
                schema: "dbo",
                table: "LeagueMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_AdminUserId",
                schema: "dbo",
                table: "Leagues",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_TournamentId",
                schema: "dbo",
                table: "Leagues",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_AwayCompetitorId",
                schema: "dbo",
                table: "Match",
                column: "AwayCompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_DependsOnMatch1Id",
                schema: "dbo",
                table: "Match",
                column: "DependsOnMatch1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_DependsOnMatch2Id",
                schema: "dbo",
                table: "Match",
                column: "DependsOnMatch2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_GroupId",
                schema: "dbo",
                table: "Match",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_HomeCompetitorId",
                schema: "dbo",
                table: "Match",
                column: "HomeCompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TournamentId",
                schema: "dbo",
                table: "Match",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_WinnerCompetitorId",
                schema: "dbo",
                table: "Match",
                column: "WinnerCompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_PointRules_TemplateId",
                schema: "dbo",
                table: "PointRules",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_MatchId",
                schema: "dbo",
                table: "Predictions",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_PredictedWinnerId",
                schema: "dbo",
                table: "Predictions",
                column: "PredictedWinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_UserId",
                schema: "dbo",
                table: "Predictions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CreatedByUserId",
                schema: "dbo",
                table: "Tournaments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TemplateId",
                schema: "dbo",
                table: "Tournaments",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTemplates_CreatedByUserId",
                schema: "dbo",
                table: "TournamentTemplates",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTiebreakers_TournamentId",
                schema: "dbo",
                table: "TournamentTiebreakers",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "dbo",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraBetOptionChoice",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ExtraBetOptionCorrectValue",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ExtraBets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "GroupCompetitors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "GroupStanding",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LeaderboardEntries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PointRules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Predictions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TournamentTiebreakers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ExtraBetOption",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LeagueMembers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Match",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Leagues",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Competitors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tournaments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TournamentTemplates",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");
        }
    }
}
