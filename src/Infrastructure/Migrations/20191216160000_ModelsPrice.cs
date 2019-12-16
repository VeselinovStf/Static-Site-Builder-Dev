using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ModelsPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "StoreTypeSites",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "StoreTypeSites",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "SiteTemplates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SiteTemplates",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "BlogTypeSites",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BlogTypeSites",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "StoreTypeSites");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "StoreTypeSites");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "SiteTemplates");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SiteTemplates");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "BlogTypeSites");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BlogTypeSites");
        }
    }
}
