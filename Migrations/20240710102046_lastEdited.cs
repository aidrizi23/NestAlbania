using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class lastEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdited",
                table: "Properties",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b18af87-3c85-4a96-a055-996b64760fa1", "AQAAAAIAAYagAAAAEKL6V+rNL2RuZYq5RGEZVIp5wQPRj62tPY7bA1QRk4qoC9PEwjRaSDbVtv38fZ+iQg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEdited",
                table: "Properties");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "58052354-9e1b-4f4b-a828-31796543f878", "AQAAAAIAAYagAAAAENd5A+3umw4JX83IntSKVnZBxOMrtUA7OGBpVrMvM3i9i/jpSXTpfyceKFrC6UWNIg==" });
        }
    }
}
