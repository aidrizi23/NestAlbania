using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "58052354-9e1b-4f4b-a828-31796543f878", "AQAAAAIAAYagAAAAENd5A+3umw4JX83IntSKVnZBxOMrtUA7OGBpVrMvM3i9i/jpSXTpfyceKFrC6UWNIg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6cbfd865-f892-423a-bb18-6c843e1fa9c9", "AQAAAAIAAYagAAAAEHqDFNxCXTJxE2h36vMqwIxAIhgJkpRvth+IGlG6IbiGTmfil5r42+6wrcvbDJbEHA==" });
        }
    }
}
