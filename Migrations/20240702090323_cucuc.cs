using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class cucuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5a450a35-3afa-4e10-9626-c9cbce90ffdc", "AQAAAAIAAYagAAAAED2aKi9kKAGhOjovIhdExCQNNo1s17gXJFpdf5R+J3gRUuNzTXASvZQEoz/ZyEJrIA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e9caf983-dba1-4a63-a3a1-efd90f9df2c7", "AQAAAAIAAYagAAAAEFPrsupepFSkZs3NvJNmewxnthjfIWwZM2dPku6myizkNpEHIYBhbOO+XYzWjRyB0w==" });
        }
    }
}
