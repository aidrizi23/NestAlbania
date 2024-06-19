using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class nest_new11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "80254130-7190-4c45-9850-e6d39e6f0c7f", "AQAAAAIAAYagAAAAECuFdhn+/OizfOq8NSRPJ6r8z/xT/9NzbLWzWowfnPGDzxyovjkYJlNixkNaXpyB5g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bc1f565d-da9c-4214-8fec-3da68f6e3770", "AQAAAAIAAYagAAAAEI9mw8f1O431lTuDVbmtktXFurz9mMmpg0GzVBbDaq4BzivpvHB4H4KiW4UkbtnXuA==" });
        }
    }
}
