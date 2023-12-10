using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class updateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Times_Appointements_appointementid",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "appointmentId",
                table: "Times");

            migrationBuilder.RenameColumn(
                name: "appointementid",
                table: "Times",
                newName: "appointementId");

            migrationBuilder.RenameIndex(
                name: "IX_Times_appointementid",
                table: "Times",
                newName: "IX_Times_appointementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Appointements_appointementId",
                table: "Times",
                column: "appointementId",
                principalTable: "Appointements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Times_Appointements_appointementId",
                table: "Times");

            migrationBuilder.RenameColumn(
                name: "appointementId",
                table: "Times",
                newName: "appointementid");

            migrationBuilder.RenameIndex(
                name: "IX_Times_appointementId",
                table: "Times",
                newName: "IX_Times_appointementid");

            migrationBuilder.AddColumn<int>(
                name: "appointmentId",
                table: "Times",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Appointements_appointementid",
                table: "Times",
                column: "appointementid",
                principalTable: "Appointements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
