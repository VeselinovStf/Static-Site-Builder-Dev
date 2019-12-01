using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class SiteTypeWidjetRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WidjetElements_Projects_AvailibleSiteWidjetId",
                table: "WidjetElements");

            migrationBuilder.DropForeignKey(
                name: "FK_WidjetElements_Projects_UsedSiteWidjetId",
                table: "WidjetElements");

            migrationBuilder.DropIndex(
                name: "IX_WidjetElements_AvailibleSiteWidjetId",
                table: "WidjetElements");

            migrationBuilder.DropIndex(
                name: "IX_WidjetElements_UsedSiteWidjetId",
                table: "WidjetElements");

            migrationBuilder.DropColumn(
                name: "AvailibleSiteWidjetId",
                table: "WidjetElements");

            migrationBuilder.DropColumn(
                name: "UsedSiteWidjetId",
                table: "WidjetElements");

            migrationBuilder.AddColumn<string>(
                name: "UsebleSiteTypeId",
                table: "Widjets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Widjets_UsebleSiteTypeId",
                table: "Widjets",
                column: "UsebleSiteTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_SiteTypes_UsebleSiteTypeId",
                table: "Widjets",
                column: "UsebleSiteTypeId",
                principalTable: "SiteTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_SiteTypes_UsebleSiteTypeId",
                table: "Widjets");

            migrationBuilder.DropIndex(
                name: "IX_Widjets_UsebleSiteTypeId",
                table: "Widjets");

            migrationBuilder.DropColumn(
                name: "UsebleSiteTypeId",
                table: "Widjets");

            migrationBuilder.AddColumn<string>(
                name: "AvailibleSiteWidjetId",
                table: "WidjetElements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsedSiteWidjetId",
                table: "WidjetElements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WidjetElements_AvailibleSiteWidjetId",
                table: "WidjetElements",
                column: "AvailibleSiteWidjetId");

            migrationBuilder.CreateIndex(
                name: "IX_WidjetElements_UsedSiteWidjetId",
                table: "WidjetElements",
                column: "UsedSiteWidjetId");

            migrationBuilder.AddForeignKey(
                name: "FK_WidjetElements_Projects_AvailibleSiteWidjetId",
                table: "WidjetElements",
                column: "AvailibleSiteWidjetId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WidjetElements_Projects_UsedSiteWidjetId",
                table: "WidjetElements",
                column: "UsedSiteWidjetId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
