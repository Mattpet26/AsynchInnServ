using Microsoft.EntityFrameworkCore.Migrations;

namespace AsynchInnServ.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ammenities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ammenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Layout = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "HotelRoom",
                columns: table => new
                {
                    HotelId = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false),
                    Rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRoom", x => new { x.HotelId, x.RoomNumber });
                    table.ForeignKey(
                        name: "FK_HotelRoom_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomAmmenities",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    AmmenityId = table.Column<int>(nullable: false),
                    AmmenitiesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAmmenities", x => new { x.AmmenityId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_RoomAmmenities_Ammenities_AmmenitiesId",
                        column: x => x.AmmenitiesId,
                        principalTable: "Ammenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomAmmenities_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ammenities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Quadruple Fridge" },
                    { 2, "Mini-Bed" },
                    { 3, "MEGA PACKAGE" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "State" },
                values: new object[,]
                {
                    { 1, "ParadiseFalls", "Yoshi-Inn", 1111111, "New-New-York" },
                    { 2, "Boonies", "Chum-City-Inn", 2222, "Flaridah" },
                    { 3, "Cold", "Third-Hotel", 1, "Alaska" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "Layout", "Name" },
                values: new object[,]
                {
                    { 1, "Green everywhere", "King suite" },
                    { 2, "For Widdle Babbies", "Jr suite" },
                    { 3, "Normal layout, idk man", "Normal suite" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoom_RoomId",
                table: "HotelRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmmenities_AmmenitiesId",
                table: "RoomAmmenities",
                column: "AmmenitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmmenities_RoomId",
                table: "RoomAmmenities",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelRoom");

            migrationBuilder.DropTable(
                name: "RoomAmmenities");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Ammenities");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
