using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class Softdeleteproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Properties",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSold",
                table: "Properties",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "JobApplications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Countries",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Agents",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "be25118c-5432-4bbd-9fc6-629e4e5fa338", "AQAAAAIAAYagAAAAENQN6LQSUY9zGoT6Lqas16LKEE8vADbP0FQKDtjoRnJo9uUDpOSixMB47QuhSWybCA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "isSold",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f95175a-6840-44fb-9e85-e994868a2871", "AQAAAAIAAYagAAAAENv06FnuQPbMG48eHGpy6DXEGg78SehI7Su7myAGaTlSGu9yhWewhBEc1CX0bXpaVA==" });
        }
    }
}
