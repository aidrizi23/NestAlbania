using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class Enum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityEnum",
                table: "Citys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8209cb66-8afd-417b-b3d5-f20466f77371", "AQAAAAIAAYagAAAAEEO8c2m82AxA1ng4eMN3dfb5bdCWp8aCqRxcgBuSCd6WaHohK9YKvgBI3yfpH7++ow==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityEnum",
                table: "Citys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "551ed4ab-8de3-4b81-8ce1-e79d59cc0945", "AQAAAAIAAYagAAAAEOfaF6gr0EmzbeMx1jltsm3sBqXJwisHQVweP4Rkbz9isSEOgzYpFH27fInN1oYqzQ==" });
        }
    }
}
