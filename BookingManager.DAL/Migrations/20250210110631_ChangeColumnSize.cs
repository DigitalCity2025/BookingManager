using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Room",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Room_image",
                table: "Room",
                column: "image",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Room_image",
                table: "Room");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Room",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }
    }
}
