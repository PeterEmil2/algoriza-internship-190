using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class updateBooking2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Times_Appointements_appointementId",
                table: "Times");

            migrationBuilder.RenameColumn(
                name: "bookingDate",
                table: "Bookings",
                newName: "bookingTime");

            migrationBuilder.AlterColumn<int>(
                name: "appointementId",
                table: "Times",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "bookingDay",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Appointements_appointementId",
                table: "Times",
                column: "appointementId",
                principalTable: "Appointements",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Times_Appointements_appointementId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "bookingDay",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "bookingTime",
                table: "Bookings",
                newName: "bookingDate");

            migrationBuilder.AlterColumn<int>(
                name: "appointementId",
                table: "Times",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Appointements_appointementId",
                table: "Times",
                column: "appointementId",
                principalTable: "Appointements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
