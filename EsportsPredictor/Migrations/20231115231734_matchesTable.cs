using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsportsPredictor.Migrations
{
    public partial class matchesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_predictions_match_match_id",
                table: "predictions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_match",
                table: "match");

            migrationBuilder.RenameTable(
                name: "match",
                newName: "matches");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "winners",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_matches",
                table: "matches",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_predictions_matches_match_id",
                table: "predictions",
                column: "match_id",
                principalTable: "matches",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_predictions_matches_match_id",
                table: "predictions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_matches",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "winners");

            migrationBuilder.RenameTable(
                name: "matches",
                newName: "match");

            migrationBuilder.AddPrimaryKey(
                name: "pk_match",
                table: "match",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_predictions_match_match_id",
                table: "predictions",
                column: "match_id",
                principalTable: "match",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
