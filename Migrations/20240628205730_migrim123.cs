using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class migrim123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fb831871-d735-4807-85f6-a4fe64d57e02", "AQAAAAIAAYagAAAAEKlHFpn5XKZ+rFOPTQGidxqvu/Ny7cP9V4GHjZv6M1b0UvmQm5uph4OWWKY4PO/Lmg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "91877b8f-ae0c-42e6-8cb7-9604dd9c5a9f", "AQAAAAIAAYagAAAAEG2uM16YUQvi58VcsfvfZz+3bear5DxAqmoSyi/P4K9Zes+9076INGfu6J5UZlEH+A==" });
        }
    }
}
