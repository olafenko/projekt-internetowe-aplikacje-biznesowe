using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class M4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Room");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "RoomType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "RoomType");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
