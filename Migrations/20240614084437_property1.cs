using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class property1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "44827b22-fbdc-4e4b-805c-7e21140cb3d9", "AQAAAAIAAYagAAAAECNMs1cidKpR5wSDMl2Wvipyw+AlXfnwFeLU9ih/Zo3vm2ilC726WFg4MzPyKEBZdQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9a8c09b8-19bf-444f-89c4-b8aee7847fb1", "AQAAAAIAAYagAAAAEE20Ly9fDf9SvMNkuYo52lgF61h5H2abRF+GNRMRTzywx6s0RrPBWkfI0YC7rjtf4A==" });
        }
    }
}
