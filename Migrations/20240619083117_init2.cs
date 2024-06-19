using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bc1f565d-da9c-4214-8fec-3da68f6e3770", "AQAAAAIAAYagAAAAEI9mw8f1O431lTuDVbmtktXFurz9mMmpg0GzVBbDaq4BzivpvHB4H4KiW4UkbtnXuA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "936321d4-557e-40a7-bc54-6173bf5e54d3", "AQAAAAIAAYagAAAAEGEB9FZtOx7IOkWA9nZgQL7y/Lpp4qw6V1GsWN2MfWx3jqbXVcFP9XRHh4LPJSPeGw==" });
        }
    }
}
