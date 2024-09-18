using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class otherimagesfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "09f53615-4dcb-4b5a-8b59-10a8c3b91a1b", "AQAAAAIAAYagAAAAEJG+ed7vKfdlh2rrbbT/FdEoz3gQvpyIGDb1iu2F9iKrdfXzu04ljSRhG1QA24HoiQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8face564-a610-49cc-ab68-a54748e09b09", "AQAAAAIAAYagAAAAECP68bnD0sJgmgI3CXWMzVDODcY70Pf91X8D7meAOCYxCsCLCqm96WksvyHDureUIQ==" });
        }
    }
}
