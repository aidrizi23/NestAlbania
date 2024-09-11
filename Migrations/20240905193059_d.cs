using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class d : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreviousPrice",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PriceChangedDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d6763337-0bca-46f0-a164-0d5636f9fa85", "AQAAAAIAAYagAAAAEJSqTWUNZCfk3Aypy4QWhUfZMKUxIBw7LvGSubJuho2IJI/0gQrz3DqRZO6ULBDvfw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousPrice",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PriceChangedDate",
                table: "Properties");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "886560a2-d836-4162-9026-6a9b15446a69", "AQAAAAIAAYagAAAAEAHH7VBTkGJper2cfINmbNOGLWCJUIwwjWzUboxz01z5wGGKI5irxJPAGJIOcfPFnA==" });
        }
    }
}
