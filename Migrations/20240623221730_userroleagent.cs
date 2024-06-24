using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class userroleagent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3a3c65e3-a4b8-4e05-baf6-86d7b9122fa1", "AQAAAAIAAYagAAAAEOI2YYKKLn04nZWcnYexzWX/YsrmMpp2TUBSEE329HfZwo8tvMMTKdGqLay1qkM++Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ed5396ba-a0c2-4c5d-bd60-d7394ddf08f7", "AQAAAAIAAYagAAAAEHndxt1WbXMTBrGA7ft/g9029Y7wLGvDx0KYbzzAnsHRmDmdweVBc3UT7mMBStZeCQ==" });
        }
    }
}
