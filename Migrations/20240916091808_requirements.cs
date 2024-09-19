using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class requirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bb6b1db1-1e71-43ba-937f-0d383d753a11", "AQAAAAIAAYagAAAAEASVUx7y+pnhdd+bzxPN6Yt5EUplGcZ0ZrMsPPS07beuZqeym8g5u/8HTzFs83shOA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6979f19a-f56d-4b9b-a67e-96bb794a96d3", "AQAAAAIAAYagAAAAEO0wpGsf6mz3uVSaXGGRSdDmCEONWtAX+9/hqds0ezV5et14QelDH/6ECuMeboplVQ==" });
        }
    }
}
