using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class jh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fd911645-964f-4e30-82e5-d8596dc7cf9e", "AQAAAAIAAYagAAAAEGGqcRvGdXYpWoZn7I5B6mbtmZIsafSMEz/c1BZzQvumhzWRQ1JtEzKWCiWs3YfvZw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d6763337-0bca-46f0-a164-0d5636f9fa85", "AQAAAAIAAYagAAAAEJSqTWUNZCfk3Aypy4QWhUfZMKUxIBw7LvGSubJuho2IJI/0gQrz3DqRZO6ULBDvfw==" });
        }
    }
}
