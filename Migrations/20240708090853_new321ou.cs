using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class new321ou : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6ad8e0c2-6d08-4269-93c0-d0be794257e7", "AQAAAAIAAYagAAAAEN8i4/5RivJi+U99jpafxtlchrvkVOBo8wDMoai8mXTBAv9nSN/H9u6EFbXVyvmU6g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79132ff8-6937-4a4c-b24b-d006a45d4a46", "AQAAAAIAAYagAAAAEHLKjHjRQ3Le+AHQKxRYtohf4EN+Hg5/1ZnCkan0Jeygpn9+tfdgEu9nOBJs5w/3oA==" });
        }
    }
}
