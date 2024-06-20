using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class citynew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ea4a985f-c0f9-4f1f-93b3-6fd304ab3142", "AQAAAAIAAYagAAAAEPB4TQsrVIFjJZztFu6Gpw8pRtgVNRvq/1dTOjclooT7LSNrogkxSBeozz8RiBaapg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c4032b55-c026-4526-a7d7-899fdd5ddfd4", "AQAAAAIAAYagAAAAEH4qYY3Mh093HfAjzfgj9QoQPYeE0QBzUdmhf6w2OZp7Menah4EGBpgaX70ZveG+DA==" });
        }
    }
}
