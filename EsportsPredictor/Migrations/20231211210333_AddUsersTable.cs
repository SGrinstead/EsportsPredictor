using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsportsPredictor.Migrations
{
    public partial class AddUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Predictions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Predictions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_ApplicationUserId",
                table: "Predictions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_AspNetUsers_ApplicationUserId",
                table: "Predictions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_AspNetUsers_ApplicationUserId",
                table: "Predictions");

            migrationBuilder.DropIndex(
                name: "IX_Predictions_ApplicationUserId",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Predictions");
        }
    }
}
