using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e9caf983-dba1-4a63-a3a1-efd90f9df2c7", "AQAAAAIAAYagAAAAEFPrsupepFSkZs3NvJNmewxnthjfIWwZM2dPku6myizkNpEHIYBhbOO+XYzWjRyB0w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba8b68e0-4a57-461a-8380-a49433f86292", "AQAAAAIAAYagAAAAEOv12JjHbYLu6UclKPar9HGipcQBSO9Fm0rDvxnkPdnPvwbU9MMH/oWxsLvxzxebzQ==" });
        }
    }
}
