using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class j : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "24d09976-b315-4353-9542-01177d536bdd", "AQAAAAIAAYagAAAAEAraumOimR0QmM4LyJa4Ki0uaxQYmkbvTyuoaIO1Gf8JD+lF88SYHFOosHH7ct5KbA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "18413c38-cc35-4175-bcfe-6d663de8f08b", "AQAAAAIAAYagAAAAEFCFqHhPyplWliavtncEFlkeD5e/OIxS3fZ0ZXS/8CaH9PclqYJwZrnQcb3QrTNKfQ==" });
        }
    }
}
