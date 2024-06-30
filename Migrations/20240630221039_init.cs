using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba8b68e0-4a57-461a-8380-a49433f86292", "AQAAAAIAAYagAAAAEOv12JjHbYLu6UclKPar9HGipcQBSO9Fm0rDvxnkPdnPvwbU9MMH/oWxsLvxzxebzQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "81d6ee31-fd48-4945-8b99-bf653ae2ab78", "AQAAAAIAAYagAAAAEBdoqFItrXDoFLa6tESQw/JS/Go/VFD+jVZQ1M739ty5rHkAv+UBBXwW44VHq+/K2w==" });
        }
    }
}
