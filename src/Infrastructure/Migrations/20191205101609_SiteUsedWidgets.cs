using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class SiteUsedWidgets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogTypeSiteId1",
                table: "Widjets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreTypeSiteId1",
                table: "Widjets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Widjets_BlogTypeSiteId1",
                table: "Widjets",
                column: "BlogTypeSiteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Widjets_StoreTypeSiteId1",
                table: "Widjets",
                column: "StoreTypeSiteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_BlogTypeSites_BlogTypeSiteId1",
                table: "Widjets",
                column: "BlogTypeSiteId1",
                principalTable: "BlogTypeSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_StoreTypeSites_StoreTypeSiteId1",
                table: "Widjets",
                column: "StoreTypeSiteId1",
                principalTable: "StoreTypeSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_BlogTypeSites_BlogTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_StoreTypeSites_StoreTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropIndex(
                name: "IX_Widjets_BlogTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropIndex(
                name: "IX_Widjets_StoreTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropColumn(
                name: "BlogTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropColumn(
                name: "StoreTypeSiteId1",
                table: "Widjets");
        }
    }
}
