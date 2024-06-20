using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class imgtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c4032b55-c026-4526-a7d7-899fdd5ddfd4", "AQAAAAIAAYagAAAAEH4qYY3Mh093HfAjzfgj9QoQPYeE0QBzUdmhf6w2OZp7Menah4EGBpgaX70ZveG+DA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "80254130-7190-4c45-9850-e6d39e6f0c7f", "AQAAAAIAAYagAAAAECuFdhn+/OizfOq8NSRPJ6r8z/xT/9NzbLWzWowfnPGDzxyovjkYJlNixkNaXpyB5g==" });
        }
    }
}
