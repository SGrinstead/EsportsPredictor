using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsportsPredictor.Migrations
{
    public partial class winnersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_predictions_winner_actual_winner_id",
                table: "predictions");

            migrationBuilder.DropForeignKey(
                name: "fk_predictions_winner_predicted_winner_id",
                table: "predictions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_winner",
                table: "winner");

            migrationBuilder.RenameTable(
                name: "winner",
                newName: "winners");

            migrationBuilder.AddPrimaryKey(
                name: "pk_winners",
                table: "winners",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_predictions_winners_actual_winner_id",
                table: "predictions",
                column: "actual_winner_id",
                principalTable: "winners",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_predictions_winners_predicted_winner_id",
                table: "predictions",
                column: "predicted_winner_id",
                principalTable: "winners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_predictions_winners_actual_winner_id",
                table: "predictions");

            migrationBuilder.DropForeignKey(
                name: "fk_predictions_winners_predicted_winner_id",
                table: "predictions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_winners",
                table: "winners");

            migrationBuilder.RenameTable(
                name: "winners",
                newName: "winner");

            migrationBuilder.AddPrimaryKey(
                name: "pk_winner",
                table: "winner",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_predictions_winner_actual_winner_id",
                table: "predictions",
                column: "actual_winner_id",
                principalTable: "winner",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_predictions_winner_predicted_winner_id",
                table: "predictions",
                column: "predicted_winner_id",
                principalTable: "winner",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
