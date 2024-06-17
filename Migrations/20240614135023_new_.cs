using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class new_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "86308b45-6565-475b-8a9a-a166984d5de0", "AQAAAAIAAYagAAAAEFxJUnUlFfP+pI7HDji07I21o6k6U3lCca8qPtidX3IB+DnE3k/V0cvJxfh3S455Tg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4aa66f1f-d8ab-47cc-8cd0-2e3dac6cb1b5", "AQAAAAIAAYagAAAAEPWgOsnmLRVfTutLqMyjAxTbPZ1qaCm/+ed9YcQ3PIy5A+KcZGnOE36WZXoqJ6qEfQ==" });
        }
    }
}
