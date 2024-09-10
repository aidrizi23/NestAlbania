using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class pikpytja1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Agents_AgentId",
                table: "Favorites");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Favorites",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8face564-a610-49cc-ab68-a54748e09b09", "AQAAAAIAAYagAAAAECP68bnD0sJgmgI3CXWMzVDODcY70Pf91X8D7meAOCYxCsCLCqm96WksvyHDureUIQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Agents_AgentId",
                table: "Favorites",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Agents_AgentId",
                table: "Favorites");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "86c5fff0-6006-489f-9051-8cacecb5865a", "AQAAAAIAAYagAAAAEO2Fg3Z25HpbmxV3ReKunVlusAXRfA7WgPtc4C/jFUkDvI8g9EM+9zljpOL1L2zb1Q==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Agents_AgentId",
                table: "Favorites",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
