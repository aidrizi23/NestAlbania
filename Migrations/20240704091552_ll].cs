using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class ll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8fb546d8-41e3-4ae0-a106-10da91dd6cda", "AQAAAAIAAYagAAAAEJnvY991xxgfxHM5hBbwa3hSUweyrow3nmODD6xf9bxnT7dSPVICl5LDp/dTG0rHmA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5a450a35-3afa-4e10-9626-c9cbce90ffdc", "AQAAAAIAAYagAAAAED2aKi9kKAGhOjovIhdExCQNNo1s17gXJFpdf5R+J3gRUuNzTXASvZQEoz/ZyEJrIA==" });
        }
    }
}
