using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6cbfd865-f892-423a-bb18-6c843e1fa9c9", "AQAAAAIAAYagAAAAEHqDFNxCXTJxE2h36vMqwIxAIhgJkpRvth+IGlG6IbiGTmfil5r42+6wrcvbDJbEHA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6ad8e0c2-6d08-4269-93c0-d0be794257e7", "AQAAAAIAAYagAAAAEN8i4/5RivJi+U99jpafxtlchrvkVOBo8wDMoai8mXTBAv9nSN/H9u6EFbXVyvmU6g==" });
        }
    }
}
