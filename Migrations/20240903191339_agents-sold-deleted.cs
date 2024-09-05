using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestAlbania.Migrations
{
    /// <inheritdoc />
    public partial class agentssolddeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agents_AgentId1",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AgentId1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AgentId1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "SoldProperties",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3c2819be-e1db-4add-ad4d-c588867eeb97", "AQAAAAIAAYagAAAAEDXPqS9sgXK9dQCkUhce87Ri9c0ln3JToTZolb0Pj1g68cLm6HY+PxPqm05CZ1820w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentId1",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoldProperties",
                table: "Agents",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0ae49e09-7b9f-4643-9b8f-86d95c33adef", "AQAAAAIAAYagAAAAEPD0OO6MFGLJk6NWyXmPIFOU3P0qG9GalINyXrixF/ebGQPCbvbuynn6cRvcRo0ARA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId1",
                table: "Properties",
                column: "AgentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agents_AgentId1",
                table: "Properties",
                column: "AgentId1",
                principalTable: "Agents",
                principalColumn: "Id");
        }
    }
}
