using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class price_pre_meter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f95175a-6840-44fb-9e85-e994868a2871", "AQAAAAIAAYagAAAAENv06FnuQPbMG48eHGpy6DXEGg78SehI7Su7myAGaTlSGu9yhWewhBEc1CX0bXpaVA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5e5f61f3-3691-49c0-bca8-f2a29bc1d410", "AQAAAAIAAYagAAAAEPqRPaQlaV8OqmftYFk4vtDdRe+HlDP/iubMaWKKLHPMKBa28fW33iQcaVD7XfzHDg==" });
        }
    }
}
