using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class userroleagent11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomUserName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "CustomUserName", "PasswordHash" },
                values: new object[] { "116a6969-bef3-483e-bf71-897998a66630", null, "AQAAAAIAAYagAAAAECgrs/xNWGLKRLUuAdFJprsu48kAYaYIGH5bc08r5fxcF3D85bt8Tk88+r970JPSgA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomUserName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3a3c65e3-a4b8-4e05-baf6-86d7b9122fa1", "AQAAAAIAAYagAAAAEOI2YYKKLn04nZWcnYexzWX/YsrmMpp2TUBSEE329HfZwo8tvMMTKdGqLay1qkM++Q==" });
        }
    }
}
