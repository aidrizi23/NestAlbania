using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class solddate : Migration
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
                values: new object[] { "94524546-5c7e-41c9-8d63-a9b276f491e6", "AQAAAAIAAYagAAAAEPXCrJgXtyolhtRoPyEVYUis1nl7uepnzVBn9HzN+Nv1xuJ9Q7qekhc1UXVTzshVog==" });
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
                values: new object[] { "24d09976-b315-4353-9542-01177d536bdd", "AQAAAAIAAYagAAAAEAraumOimR0QmM4LyJa4Ki0uaxQYmkbvTyuoaIO1Gf8JD+lF88SYHFOosHH7ct5KbA==" });
        }
    }
}
