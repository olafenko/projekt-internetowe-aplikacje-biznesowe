using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class M2ModelsChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityRoomType");

            migrationBuilder.DropTable(
                name: "ReservationService");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "RoomType");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Payment",
                newName: "Amount");

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "RoomType",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Room",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomStatus",
                table: "Room",
                type: "nvarchar(25)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AdultCount",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildCount",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Guest",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCardNumber",
                table: "Guest",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Guest",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Amenity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Amenity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdditionalOfferReservation",
                columns: table => new
                {
                    AdditionalOffersId = table.Column<int>(type: "int", nullable: false),
                    ReservationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalOfferReservation", x => new { x.AdditionalOffersId, x.ReservationsId });
                    table.ForeignKey(
                        name: "FK_AdditionalOfferReservation_Reservation_ReservationsId",
                        column: x => x.ReservationsId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionalOfferReservation_Service_AdditionalOffersId",
                        column: x => x.AdditionalOffersId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmenityRoom",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>(type: "int", nullable: false),
                    RoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityRoom", x => new { x.AmenitiesId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_AmenityRoom_Amenity_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "Amenity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenityRoom_Room_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalOfferReservation_ReservationsId",
                table: "AdditionalOfferReservation",
                column: "ReservationsId");

            migrationBuilder.CreateIndex(
                name: "IX_AmenityRoom_RoomsId",
                table: "AmenityRoom",
                column: "RoomsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalOfferReservation");

            migrationBuilder.DropTable(
                name: "AmenityRoom");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "RoomType");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomStatus",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "AdultCount",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ChildCount",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Guest");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Amenity");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Amenity");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payment",
                newName: "Price");

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "RoomType",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "Room",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Guest",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCardNumber",
                table: "Guest",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "AmenityRoomType",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>(type: "int", nullable: false),
                    RoomTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityRoomType", x => new { x.AmenitiesId, x.RoomTypesId });
                    table.ForeignKey(
                        name: "FK_AmenityRoomType_Amenity_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "Amenity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenityRoomType_RoomType_RoomTypesId",
                        column: x => x.RoomTypesId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationService",
                columns: table => new
                {
                    ReservationsId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationService", x => new { x.ReservationsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ReservationService_Reservation_ReservationsId",
                        column: x => x.ReservationsId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationService_Service_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenityRoomType_RoomTypesId",
                table: "AmenityRoomType",
                column: "RoomTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationService_ServicesId",
                table: "ReservationService",
                column: "ServicesId");
        }
    }
}
