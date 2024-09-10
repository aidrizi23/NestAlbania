using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class pikpytja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Properties");

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "86c5fff0-6006-489f-9051-8cacecb5865a", "AQAAAAIAAYagAAAAEO2Fg3Z25HpbmxV3ReKunVlusAXRfA7WgPtc4C/jFUkDvI8g9EM+9zljpOL1L2zb1Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AgentId",
                table: "Favorites",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Agents_AgentId",
                table: "Favorites",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Agents_AgentId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_AgentId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Favorites");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b3db5ec4-bd49-45cc-ac6a-9034348b80a3", "AQAAAAIAAYagAAAAEEhspy7dsBSMyohKCdmekc3hKhvOE/catlsVk9JhXWvn6+axSOcF4450OodHtmxrRA==" });
        }
    }
}
