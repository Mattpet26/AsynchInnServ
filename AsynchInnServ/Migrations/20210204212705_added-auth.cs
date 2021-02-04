using Microsoft.EntityFrameworkCore.Migrations;

namespace AsynchInnServ.Migrations
{
    public partial class addedauth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "districtmanager", "00000000-0000-0000-0000-000000000000", "DistrictManager", "DISTRICTMANAGER" },
                    { "propertymanager", "00000000-0000-0000-0000-000000000000", "PropertyManager", "PROPERTYMANAGER" },
                    { "agent", "00000000-0000-0000-0000-000000000000", "Agent", "AGENT" },
                    { "guest", "00000000-0000-0000-0000-000000000000", "Guest", "GUEST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "create", "districtmanager" },
                    { 2, "permissions", "update", "districtmanager" },
                    { 3, "permissions", "delete", "districtmanager" },
                    { 4, "permissions", "create", "propertymanager" },
                    { 5, "permissions", "create", "agent" },
                    { 6, "permissions", "update", "agent" },
                    { 7, "permissions", "delete", "agent" },
                    { 8, "permissions", "create", "guest" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "agent");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "districtmanager");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "guest");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "propertymanager");
        }
    }
}
