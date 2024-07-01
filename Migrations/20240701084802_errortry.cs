using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class errortry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9e8f6d1c-96f3-4988-a8bb-38fe42b87cd3", "AQAAAAIAAYagAAAAEE5VZSH0Bk81EYMAxZySKE8x73NigtovHnNTfPTglVDFwTSVRdedwTfZ8rkY08IlHQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "91877b8f-ae0c-42e6-8cb7-9604dd9c5a9f", "AQAAAAIAAYagAAAAEG2uM16YUQvi58VcsfvfZz+3bear5DxAqmoSyi/P4K9Zes+9076INGfu6J5UZlEH+A==" });
        }
    }
}
