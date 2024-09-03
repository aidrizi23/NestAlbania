using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class softdeleteagent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash" },
                values: new object[] { "3821c8c8-1905-4a3d-802b-8fe50e15a580", null, "AQAAAAIAAYagAAAAEAQ1boHRzijxoE8V/UvFLUJ71fgkhfVko9dVRy2tCydtbBhZ2ClBkS2X6c9nlM4GeA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3c2819be-e1db-4add-ad4d-c588867eeb97", "AQAAAAIAAYagAAAAEDXPqS9sgXK9dQCkUhce87Ri9c0ln3JToTZolb0Pj1g68cLm6HY+PxPqm05CZ1820w==" });
        }
    }
}
