using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EsportsPredictor.Migrations
{
    public partial class dbSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "match",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    slug = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    begin_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    match_type = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    winner_type = table.Column<string>(type: "text", nullable: false),
                    winner_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_match", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "winner",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    details_page_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_winner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "predictions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    match_id = table.Column<int>(type: "integer", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    predicted_winner_id = table.Column<int>(type: "integer", nullable: false),
                    actual_winner_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_predictions", x => x.id);
                    table.ForeignKey(
                        name: "fk_predictions_match_match_id",
                        column: x => x.match_id,
                        principalTable: "match",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_predictions_winner_actual_winner_id",
                        column: x => x.actual_winner_id,
                        principalTable: "winner",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_predictions_winner_predicted_winner_id",
                        column: x => x.predicted_winner_id,
                        principalTable: "winner",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_predictions_actual_winner_id",
                table: "predictions",
                column: "actual_winner_id");

            migrationBuilder.CreateIndex(
                name: "ix_predictions_match_id",
                table: "predictions",
                column: "match_id");

            migrationBuilder.CreateIndex(
                name: "ix_predictions_predicted_winner_id",
                table: "predictions",
                column: "predicted_winner_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "predictions");

            migrationBuilder.DropTable(
                name: "match");

            migrationBuilder.DropTable(
                name: "winner");
        }
    }
}
