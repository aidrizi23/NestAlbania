using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class newrequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fef40193-1546-42a2-9e0a-1ba9456cf0e6", "AQAAAAIAAYagAAAAEH4/mjgzlCFi9GnSdnsnbd95cUo2pOYVMXYaVFJP3zMEcZZjxT949f4UvCtgWxDRgQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "569c4070-5a05-4421-b00e-0e9d7082d2d6", "AQAAAAIAAYagAAAAEB6581HmIy4NhUy6Ge5Vlkc/yscQ9lF5ZeiEQnUDe2biN6HoGtZH3L/w5edRrGLk8Q==" });
        }
    }
}
