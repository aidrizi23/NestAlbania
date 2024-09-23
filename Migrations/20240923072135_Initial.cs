using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SoldDate",
                table: "Properties",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "db4485e3-fda8-4575-8143-f4b00b5c9910", "AQAAAAIAAYagAAAAEGkzLdrSUCmO1xHGBzpefIFcRmWkk8Br3LalFTnZbOg3dEVm0WJUTgECp8/9hr6beg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoldDate",
                table: "Properties");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0df43ce4-357a-487e-a687-823a86ffa7c0", "AQAAAAIAAYagAAAAEEoLT+rd/eSoXw0CXAFgLML6yXg7NRjspA4REy7H8FZ8f1hbvs2rXsalzwzNrdlpfQ==" });
        }
    }
}
