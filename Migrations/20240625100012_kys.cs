using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class kys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "65dc27b2-4078-46c5-bfdf-cef8ac0c44bc", "AQAAAAIAAYagAAAAELwAQ+qu86yRjhqW7Uz4+vE0oAE4bmDGscMwinJh4ymEOzUjQGtw3Oz6CG1M1A/Edg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "116a6969-bef3-483e-bf71-897998a66630", "AQAAAAIAAYagAAAAECgrs/xNWGLKRLUuAdFJprsu48kAYaYIGH5bc08r5fxcF3D85bt8Tk88+r970JPSgA==" });
        }
    }
}
